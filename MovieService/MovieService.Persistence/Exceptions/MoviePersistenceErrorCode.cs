namespace MovieService.Persistence.Exceptions;

/// <summary>
/// An enum for specifying error codes addressing the occured problem in persistence. 
/// </summary>
public enum MoviePersistenceErrorCode
{
    MovieNotFound,
    MovieUpdateFailed,
    MovieDeletionFailed,
    MovieAdditionFailed
}