namespace VenueService.Domain.Exceptions;

public enum VenueDomainErrorCode
{
    NegativeLayoutWidth,
    LayoutWidthOverflow,
    MaximumLayoutHeightOverflow,
    TheaterCapacityIsFull,
    SessionEnded,
    SeatOccupied,
    SeatIsNotOccupied,
    SeatDoesNotExist,
    SessionTimeRangeOverlap,
    SessionDoesNotExist,
    NegativePriceAmount,
    SeatRowOutOfBounds,
    SeatVersionExpired,
}