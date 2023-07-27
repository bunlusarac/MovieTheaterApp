using VenueService.Domain.Entities;
using VenueService.Domain.Exceptions;
using VenueService.Domain.Utils;
using VenueService.Domain.ValueObjects;

namespace VenueService.Test;

public class VenueTests
{
    [Fact]
    public void TheaterLayoutCannotTakeNegativeWidth()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        Assert.Throws<VenueDomainException>(() =>
            venue.AddTheater("Test Theater", -1, TheaterType.Standard2D));
    }
    
    [Fact]
    public void AddingSeatsInRowLayoutCannotSurpassTheaterLayoutWidth()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        Assert.Throws<VenueDomainException>(() => theater.Layout.AddRow(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }));
    }
    
    [Fact]
    public void AddingDoubleSeatsOccupyTwoSeatWidths()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 2, TheaterType.Standard2D);
        var theater = venue.Theaters[0];
        
        var exception = Record.Exception(() => theater.Layout.AddRow(new()
        {
                SeatType.Double,
        }));
        
        Assert.Null(exception);
    }
    
    [Fact]
    public void AddingRowLayoutCannotSurpassMaximumTheaterLayoutHeight()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        Assert.Throws<VenueDomainException>(() => theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 26 + 1));
    }
    
    [Fact]
    public void SeatCannotBeOccupiedIfSessionCapacityIsFull()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        var session = theater.AddSession(new TimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)), Guid.NewGuid(),
            Localization.Subtitle, new List<Pricing>() { new(TicketType.Standard, new Price(1)) });

        // Because DB creates the GUIDs, seat GUIDs are created manually in testing.
        // SetSeat uses GUID, so without GUID it will throw an exception prior to the
        // asserted exception in this test case.
        foreach (var seat in session.SeatingState.StateSeats)
        {
            seat.Id = Guid.NewGuid();
        }
        
        for (var i = 'A' ; i <= 'C'; i++)
        {
            for (var j = 1; j < 4; j++)
            {
                session.OccupySeat(i, j);
            }
        }

        Assert.Throws<VenueDomainException>(() => session.OccupySeat('A', 1));
    }
    
    [Fact]
    public void SeatCannotBeOccupiedIfSessionHasEnded()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        var session = theater.AddSession(
            new TimeRange(DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)), DateTime.UtcNow),
            Guid.NewGuid(), 
            Localization.Subtitle, 
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) });

        Assert.Throws<VenueDomainException>(() => session.OccupySeat('A', 1));
    }
    
    [Fact]
    public void SeatCannotBeOccupiedIfItIsOccupied()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        var session = theater.AddSession(
            new TimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
            Guid.NewGuid(), 
            Localization.Subtitle, 
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) });
        
        session.OccupySeat('A', 1);
        Assert.Throws<VenueDomainException>(() => session.OccupySeat('A', 1));
    }
    
    [Fact]
    public void SeatCannotBeOccupiedIfItIsNotOccupied()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        var session = theater.AddSession(
            new TimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
            Guid.NewGuid(), 
            Localization.Subtitle, 
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) });
        
        Assert.Throws<VenueDomainException>(() => session.ReleaseSeat('A', 1));
    }
    
    [Fact]
    public void SessionCannotBeCreatedIfItOverlapsWithAnother()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        theater.AddSession(
            new TimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
            Guid.NewGuid(), 
            Localization.Subtitle, 
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) });

        Assert.Throws<VenueDomainException>(() => theater.AddSession(
            new TimeRange(DateTime.UtcNow.AddHours(0.5), DateTime.UtcNow.AddHours(1.5)),
            Guid.NewGuid(),
            Localization.Subtitle,
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) })
        );
    }
    
    [Fact]
    public void SessionCannotBeDeletedIfItDoesNotExist()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        Assert.Throws<VenueDomainException>(() => theater.DeleteSession(new Guid()));
    }
    
    [Fact]
    public void PriceAmountCannotBeNegative()
    {
        Assert.Throws<VenueDomainException>(() => new Price(decimal.Negate(decimal.One)));
    }
    
    [Fact]
    public void OutOfBoundsSeatsCannotBeRetrieved()
    {
        var venue = new Venue("Test Venue", Location.Canakkale);
        
        venue.AddTheater("Test Theater", 3, TheaterType.Standard2D);
        var theater = venue.Theaters[0];

        theater.Layout.AddRows(new()
        {
            SeatType.Single,
            SeatType.Single,
            SeatType.Single
        }, 3);
        
        var session = theater.AddSession(
            new TimeRange(DateTime.UtcNow, DateTime.UtcNow.AddHours(1)),
            Guid.NewGuid(), 
            Localization.Subtitle, 
            new List<Pricing>() { new(TicketType.Standard, new Price(1)) });
        
        Assert.Throws<VenueDomainException>(() => session.SeatingState.GetSeat('G', 5));
    }
}