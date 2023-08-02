namespace LoyaltyService.Domain.Common;

public class EntityBase
{
    public Guid Id { get; }

    public override bool Equals(object? obj)
    {
        var other = obj as EntityBase;

        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(other, this))
            return true;

        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }

    public static bool operator ==(EntityBase? left, EntityBase? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;
        
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }
    
    public static bool operator !=(EntityBase left, EntityBase right)
    {
        return !(left == right);
    }
}