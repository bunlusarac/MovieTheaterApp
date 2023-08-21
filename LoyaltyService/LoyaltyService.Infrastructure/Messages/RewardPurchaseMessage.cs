namespace LoyaltyService.Infrastructure.Messages;

public class RewardPurchaseMessage: RabbitMessage
{
    public override RabbitMessageEvent Event { get; set; } = RabbitMessageEvent.EVENT_REWARD_PURCHASE;
    public Guid CustomerId { get; set; }
    public decimal PointsAmount { get; set; }

    public RewardPurchaseMessage(Guid customerId, decimal pointsAmount)
    {
        CustomerId = customerId;
        PointsAmount = pointsAmount;
    }
}