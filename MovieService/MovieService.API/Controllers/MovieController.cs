using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieService.API.DTOs;
using MovieService.Domain.Entities;
using MovieService.Domain.Exceptions;
using MovieService.Persistence.Exceptions;
using MovieService.Persistence.Repositories;

namespace MovieService.API.Controllers;

/// <summary>
/// CRUD controller for Movie entities.
/// </summary>
[ApiController]
[Route("movies")]
public class MovieController: ControllerBase
{
    private readonly MovieRepository _movieRepository;
    
    public MovieController(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    /// <summary>
    /// Get all movies.
    /// </summary>
    /// <returns>List of all Movie entities.</returns>
    [HttpGet]
    public async Task<List<Movie>> GetAllMovies()
    {
        return await _movieRepository.GetAllAsync();
    }

    /// <summary>
    /// Get a Movie entity by its ID.
    /// </summary>
    /// <param name="movieId">ID of the sought movie.</param>
    /// <returns></returns>
    [HttpGet("{movieId:guid}")]
    public async Task<Movie> GetMovieById(Guid movieId)
    {
    
        return await _movieRepository.GetByIdAsync(movieId);
    }
    
    /// <summary>
    /// Create a new Movie entity.
    /// </summary>
    /// <param name="movieDto">Object of properties of Movie entity to be created.</param>
    [HttpPost("")]
    public async Task AddMovie([FromBody] AddMovieDto movieDto)
    {
        var movie = new Movie(
            movieDto.Name,
            movieDto.Director,
            movieDto.Actors,
            movieDto.Genre,
            movieDto.Summary,
            movieDto.PosterImageUri,
            movieDto.SmartSigns,
            movieDto.Formats,
            movieDto.ReleaseStatus,
            movieDto.ReleaseDate);
        
        await _movieRepository.AddAsync(movie);
    }
    
    /// <summary>
    /// Update a Movie entity of given ID.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be updated.</param>
    [HttpPut("{movieId:guid}")]
    public async Task UpdateMovie(Guid movieId, [FromBody] UpdateMovieDto movieDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);

        movie.Name = movieDto.Name;
        movie.Director = movieDto.Director;
        movie.Actors = movieDto.Actors;
        movie.Genre = movieDto.Genre;
        movie.Summary = movieDto.Summary;
        movie.PosterImageUri = movieDto.PosterImageUri;
        movie.SmartSigns = movieDto.SmartSigns;
        movie.Formats = movieDto.Formats;
    }
    
    /// <summary>
    /// Delete a Movie entity of given ID.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be deleted.</param>
    /// <returns>Truth value regarding to success of delete operation.</returns>
    [HttpDelete("{movieId:guid}")]
    public async Task DeleteMovie(Guid movieId)
    {
        await _movieRepository.DeleteAsync(movieId);
    } 
    
    /// <summary>
    /// Set the movie as upcoming, i.e. as an unreleased movie with determined release date in the future.
    /// At releaseDate, this movie is planned to be in the release status <c>ReleaseStatus.InTheaters</c>.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be set as upcoming.</param>
    /// <param name="movieDto">Object that contains DateTime of release date.</param>
    [HttpPut("{movieId:guid}/upcoming")]
    public async Task SetMovieAsUpcoming(Guid movieId, [FromBody] SetMovieAsUpcomingDto movieDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        movie.SetAsUpcoming(movieDto.ReleaseDate);
        await _movieRepository.UpdateAsync(movie);
        
        // Schedule Hangfire job
        BackgroundJob.Schedule(() => SetMovieAsInTheatersJob(movieId), movieDto.ReleaseDate - DateTime.UtcNow);
    }

    /// <summary>
    /// Hangfire job method responsible for setting the movie as In Theaters on the release date.
    /// </summary>
    /// <param name="movieId"></param>
    [NonAction]
    public void SetMovieAsInTheatersJob(Guid movieId)
    {
        var movie = _movieRepository.GetById(movieId);
        movie.SetAsInTheaters();
        _movieRepository.Update(movie);
    }

    /// <summary>
    /// Deletes all jobs of a Movie entity with given job title registered to Hangfire.
    /// </summary>
    /// <param name="jobName">Name of jobs to be deleted</param>
    /// <param name="movieId">ID of the Movie entity whose jobs are to be deleted</param>
    [NonAction]
    public void DeleteJob(string jobName, Guid movieId)
    {
        var monitor = JobStorage.Current.GetMonitoringApi();
        var jobsScheduled = monitor.ScheduledJobs(0, int.MaxValue)
            .Where(x => x.Value.Job.Method.Name == jobName);

        foreach (var j in jobsScheduled)
        {
            var jobMovieId = (Guid) j.Value.Job.Args[0];
            if (jobMovieId == movieId) BackgroundJob.Delete(j.Key);
        }
    }
    
    /// <summary>
    /// Set the movie as in theaters, i.e. as a recently released movie.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be set as in theaters.</param>
    [HttpPut("{movieId:guid}/in-theaters")]
    public async Task SetMovieAsInTheaters(Guid movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        movie.SetAsInTheaters();
        await _movieRepository.UpdateAsync(movie);
        
        // Delete scheduled Hangfire job if it exists
        DeleteJob("SetMovieAsInTheatersJob", movieId);
    }
    
    /// <summary>
    /// Set the movie as released, i.e. as a movie that has been released a long time ago.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be set as released.</param>
    [HttpPut("{movieId:guid}/released")]
    public async Task SetMovieAsReleased(Guid movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        movie.SetAsReleased();
        await _movieRepository.UpdateAsync(movie);
        
        // Delete scheduled Hangfire job if it exists
        DeleteJob("SetMovieAsInTheatersJob", movieId);
    }
    
    /// <summary>
    /// Update the movie's release date to a given date.
    /// </summary>
    /// <param name="movieId">ID of Movie entity whose release date will be updated.</param>
    /// <param name="releaseDateDto">Object containing information about new release date.</param>
    [HttpPut("{movieId:guid}/release-date")]
    public async Task SetMovieReleaseDate(Guid movieId, SetReleaseDateDto releaseDateDto)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        movie.ReleaseDate = releaseDateDto.ReleaseDate;
        await _movieRepository.UpdateAsync(movie);
    }

    /// <summary>
    /// Rate a movie with given ID, with the provided score.
    /// </summary>
    /// <param name="movieId">ID of Movie entity to be rated.</param>
    /// <param name="rating">Rating score in the interval [1, 10]</param>
    [HttpPut("{movieId:guid}/rate/{rating:int}")]
    public async Task RateMovie(Guid movieId, int rating)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId);
        movie.UpdateRating(rating);
        await _movieRepository.UpdateAsync(movie);
    }
}