using BookingService.Application.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BookingService.Infrastructure.Messages;

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
            /*
            case RabbitMessageEvent.EVEN:
                var message = JsonConvert.DeserializeObject<UserRegisteredMessage>(messageJson);

                if (message is not null)
                {
                    using (var scope = _serviceProvider.CreateAsyncScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new RegisterLoyaltyCustomerCommand(message.UserId);
                        await mediator.Send(command);    
                    }
                }
                
                break;
                */
            default:
                break;
        }
    }
}