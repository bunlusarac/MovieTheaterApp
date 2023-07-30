using MongoDB.Driver;
using MovieService.Domain.Entities;
using MovieService.Persistence.Exceptions;

namespace MovieService.Persistence.Repositories;

/// <summary>
/// Repository for accessing Movie entities from MongoDB collection.
/// </summary>
public class MovieRepository
{
    private readonly IMongoCollection<Movie> _movieCollection;

    public MovieRepository(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(dbName);

        _movieCollection = db.GetCollection<Movie>("Movies");
    }

    /// <summary>
    /// Get all Movie entities.
    /// </summary>
    /// <returns>List of all Movie entities.</returns>
    public async Task<List<Movie>> GetAllAsync()
    {
        return await _movieCollection.Find(_ => true).ToListAsync();
    }
    
    
    /// <summary>
    /// Add a Movie entity.
    /// </summary>
    /// <param name="movie">Movie to be added</param>
    /// <exception cref="MoviePersistenceException">Thrown when movie addition fails.</exception>
    public async Task AddAsync(Movie movie)
    {
        try
        {
            await _movieCollection.InsertOneAsync(movie);
        }
        catch (Exception exception)
        {
            throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieAdditionFailed, exception);
        }
    }
    
    /// <summary>
    /// Get a Movie entity by its ID.
    /// </summary>
    /// <param name="id">ID of the sought Movie.</param>
    /// <returns>The sought Movie entity.</returns>
    /// <exception cref="MoviePersistenceException">Thrown when movie with given ID is not found.</exception>
    public async Task<Movie> GetByIdAsync(Guid id)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
        var movie = await _movieCollection.Find(filter).FirstOrDefaultAsync();
        if (movie == null) throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieNotFound);
        return movie;
    }

    /// <summary>
    /// Update an existing Movie entity.
    /// </summary>
    /// <param name="movie">The updated Movie entity.</param>
    /// <exception cref="MoviePersistenceException">Thrown when update fails.</exception>
    public async Task UpdateAsync(Movie movie)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, movie.Id);
        var result = await _movieCollection.ReplaceOneAsync(filter, movie);

        if (!result.IsModifiedCountAvailable || result.ModifiedCount <= 0)
            throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieUpdateFailed);
    }
    
    /// <summary>
    /// Delete an existing Movie entity.
    /// </summary>
    /// <param name="id">ID of the Movie entity to be deleted.</param>
    /// <exception cref="MoviePersistenceException">Thrown when deletion fails.</exception>
    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
        var result = await _movieCollection.DeleteOneAsync(filter);

        if (result.DeletedCount <= 0)
            throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieDeletionFailed);
    }

    /// <summary>
    /// Synchronous version of GetMovieByIdAsync for contexts where async calls are not allowed.
    /// </summary>
    /// <param name="id">ID of the sought Movie.</param>
    /// <returns>The sought Movie entity.</returns>
    /// <exception cref="MoviePersistenceException">Thrown when movie with given ID is not found.</exception>
    public Movie GetById(Guid id)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
        var movie = _movieCollection.Find(filter).FirstOrDefault();
        if (movie == null) throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieNotFound);
        return movie; 
    }
    
    /// <summary>
    /// Synchronous version of UpdateAsync for contexts where async calls are not allowed.
    /// </summary>
    /// <param name="movie">The updated Movie entity.</param>
    /// <exception cref="MoviePersistenceException">Thrown when update fails.</exception>
    public void Update(Movie movie)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, movie.Id);
        var result = _movieCollection.ReplaceOne(filter, movie);

        if (!result.IsModifiedCountAvailable || result.ModifiedCount <= 0)
            throw new MoviePersistenceException(MoviePersistenceErrorCode.MovieUpdateFailed);
    }
}