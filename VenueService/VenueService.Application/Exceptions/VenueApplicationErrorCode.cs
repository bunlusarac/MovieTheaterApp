namespace VenueService.Application.Exceptions;

public enum VenueApplicationErrorCode
{
    VenueDoesNotExist,
    TheaterDoesNotExist,
    SessionDoesNotExist,
    SeatDoesNotExist,
    SeatVersionOutdated,
}