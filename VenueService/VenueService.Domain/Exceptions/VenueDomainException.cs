using Microsoft.AspNetCore.Http;

namespace VenueService.Domain.Exceptions;

public class VenueDomainException: Exception
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }

    public VenueDomainException(VenueDomainErrorCode errorCode)
    {
        switch (errorCode)
        {
            case VenueDomainErrorCode.NegativeLayoutWidth:
                Type = "https://docs.venue.com/errors/negative-layout-width";
                Title = "Negative layout width specified";
                Status = StatusCodes.Status400BadRequest;
                Detail = "A theater layout cannot take negative width.";
                
                break;
            
            case VenueDomainErrorCode.LayoutWidthOverflow:
                Type = "https://docs.venue.com/errors/layout-width-overflow";
                Title = "Layout width overflowed";
                Status = StatusCodes.Status400BadRequest;
                Detail = "Amount of layout seats added surpassed the maximum possible number of seats in this row.";
                
                break;
            
            case VenueDomainErrorCode.MaximumLayoutHeightOverflow:
                Type = "https://docs.venue.com/errors/max-layout-width-overflow";
                Title = "Layout height overflowed";
                Status = StatusCodes.Status400BadRequest;
                Detail = "Amount of layout rows added surpassed the maximum possible number of rows in this theater.";
                
                break;
            
            case VenueDomainErrorCode.TheaterCapacityIsFull:
                Type = "https://docs.venue.com/errors/theater-capacity-full";
                Title = "Theater capacity full";
                Status = StatusCodes.Status403Forbidden;
                Detail = "There is not enough capacity in this session to reserve a seat.";
                
                break;
            
            case VenueDomainErrorCode.SessionEnded:
                Type = "https://docs.venue.com/errors/session-ended";
                Title = "Session ended";
                Status = StatusCodes.Status403Forbidden;
                Detail = "This session has ended and no further seat operations can be performed.";
                
                break;
            
            case VenueDomainErrorCode.SeatOccupied:
                Type = "https://docs.venue.com/errors/seat-occupied";
                Title = "Seat is occupied";
                Status = StatusCodes.Status403Forbidden;
                Detail = "Specified seat in the session has been occupied already.";
                
                break;
            
            case VenueDomainErrorCode.SeatIsNotOccupied:
                Type = "https://docs.venue.com/errors/seat-not-occupied";
                Title = "Seat is not occupied";
                Status = StatusCodes.Status400BadRequest;
                Detail = "Specified seat to release is not occupied.";
                
                break;
            
            case VenueDomainErrorCode.SeatDoesNotExist:
                Type = "https://docs.venue.com/errors/seat-not-exist";
                Title = "Seat does not exist";
                Status = StatusCodes.Status404NotFound;
                Detail = "Specified seat does not exist in the theater.";
                
                break;
            
            case VenueDomainErrorCode.SessionTimeRangeOverlap:
                Type = "https://docs.venue.com/errors/session-timerange-overlap";
                Title = "Session time range overlap";
                Status = StatusCodes.Status403Forbidden;
                Detail = "There is a scheduled session in this theater on the specified time range.";
                
                break;
            
            case VenueDomainErrorCode.SessionDoesNotExist:
                Type = "https://docs.venue.com/errors/session-not-exist";
                Title = "Session does not exist";
                Status = StatusCodes.Status404NotFound;
                Detail = "There is no such session for given session ID.";
                
                break;
            
            case VenueDomainErrorCode.NegativePriceAmount:
                Type = "https://docs.venue.com/errors/negative-price-amount";
                Title = "Negative price amount";
                Status = StatusCodes.Status400BadRequest;
                Detail = "Price amount cannot be negative.";
                
                break;
            case VenueDomainErrorCode.SeatRowOutOfBounds:
                Type = "https://docs.venue.com/errors/seat-row-oob";
                Title = "Seat row is out of bounds";
                Status = StatusCodes.Status400BadRequest;
                Detail = "Row identifier must be between A and Z.";
                
                break;
            default:
                Type = "";
                Title = "Venue domain exception";
                Status = StatusCodes.Status400BadRequest;
                Detail = "An exception occured on the venue domain.";
                
                break;
        }
    }
}