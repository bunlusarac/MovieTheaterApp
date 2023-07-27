using MongoDB.Driver;
using MovieService.Domain.Entities;

namespace MovieService.Persistence.Repositories;

public class MovieRepository
{
    private readonly IMongoCollection<Movie> _movieCollection;

    public MovieRepository(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(dbName);

        _movieCollection = db.GetCollection<Movie>("Movie");
    }

    public async Task AddAsync(Movie movie)
    {
        await _movieCollection.InsertOneAsync(movie);
    }

    public async Task<Movie> GetByIdAsync(Guid id)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
        return await _movieCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(Movie movie)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, movie.Id);
        var result = await _movieCollection.ReplaceOneAsync(filter, movie);

        return result.IsModifiedCountAvailable && result.ModifiedCount > 0;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.Id, id);
        var result = await _movieCollection.DeleteOneAsync(filter);
        
        return result.DeletedCount > 0;
    }
}