using Microsoft.AspNetCore.Http;

namespace VenueService.Application.Exceptions;

public class VenueApplicationException: Exception
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
    
    public VenueApplicationException(VenueApplicationErrorCode errorCode)
    {
        switch (errorCode)
        {
            case VenueApplicationErrorCode.VenueDoesNotExist:
                Type = "https://docs.venue.com/errors/venue-not-exist";
                Title = "Venue does not exist";
                Status = StatusCodes.Status404NotFound;
                Detail = "There is no such venue for given session ID.";
                
                break;
            case VenueApplicationErrorCode.TheaterDoesNotExist:
                Type = "https://docs.venue.com/errors/theater-not-exist";
                Title = "Theater does not exist";
                Status = StatusCodes.Status404NotFound;
                Detail = "There is no such theater for given session ID.";
                
                break;
            case VenueApplicationErrorCode.SessionDoesNotExist :
                Type = "https://docs.venue.com/errors/session-not-exist";
                Title = "Session does not exist";
                Status = StatusCodes.Status404NotFound;
                Detail = "There is no such session for given session ID.";
                
                break;
            default:
                Type = "";
                Title = "Venue application exception";
                Status = StatusCodes.Status400BadRequest;
                Detail = "An exception occured on the venue application.";
                
                break;
        }
    }
}