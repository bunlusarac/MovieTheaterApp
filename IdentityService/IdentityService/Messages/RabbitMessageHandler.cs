using IdentityService.Commands;
using MediatR;
using Newtonsoft.Json;

namespace IdentityService.Messages;

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
            case RabbitMessageEvent.EVENT_OTP_VALIDATED:
                var message = JsonConvert.DeserializeObject<OtpValidatedMessage>(messageJson);

                if (message is not null)
                {
                    using (var scope = _serviceProvider.CreateAsyncScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new CreateShortSessionCommand(message.UserId);
                        
                        await mediator.Send(command);    
                    }
                }
                
                break;
            
            default:
                break;
        }
    }
}