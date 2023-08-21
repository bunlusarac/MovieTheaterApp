using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using LoyaltyService.Application.Commands;

namespace LoyaltyService.Infrastructure.Messages;

public class RabbitMessageHandler: IRabbitMessageHandler
{
    /*
    private readonly IMediator _mediator;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMessageHandler(IMediator mediator, IServiceProvider serviceProvider)
    {
        _mediator = mediator;
        _serviceProvider = serviceProvider;
    }
    */
    
    private readonly IServiceProvider _serviceProvider;

    public RabbitMessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(RabbitMessageEvent messageEvent, string messageJson)
    {
        switch (messageEvent)
        {
            case RabbitMessageEvent.EVENT_USER_REGISTERED:
                var userRegisteredMessage = JsonConvert.DeserializeObject<UserRegisteredMessage>(messageJson);

                if (userRegisteredMessage is not null)
                {
                    using (var scope = _serviceProvider.CreateAsyncScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new RegisterLoyaltyCustomerCommand(userRegisteredMessage.UserId);
                        await mediator.Send(command);    
                    }
                }
                
                break;
            case RabbitMessageEvent.EVENT_REWARD_PURCHASE:
                var rewardPurchaseMessage = JsonConvert.DeserializeObject<RewardPurchaseMessage>(messageJson);

                if (rewardPurchaseMessage is not null)
                {
                    using (var scope = _serviceProvider.CreateAsyncScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new DepositToWalletCommand(rewardPurchaseMessage.CustomerId, rewardPurchaseMessage.PointsAmount);
                        await mediator.Send(command);    
                    }
                }
                
                break;
            default:
                break;
        }
    }
}