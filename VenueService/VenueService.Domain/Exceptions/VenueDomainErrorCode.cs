namespace VenueService.Domain.Exceptions;

public enum VenueDomainErrorCode
{
    NegativeLayoutWidth,
    LayoutWidthOverflow,
    LayoutHeightOverflow,
    TheaterCapacityIsFull,
    SessionEnded,
    SeatOccupied,
    SeatIsNotOccupied,
    SeatDoesNotExist,
    SessionTimeRangeOverlap,
    SessionDoesNotExist,
    NegativePriceAmount,
    SeatRowOutOfBounds,
}