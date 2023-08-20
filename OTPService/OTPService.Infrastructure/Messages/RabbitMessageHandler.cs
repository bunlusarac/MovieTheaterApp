using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OTPService.Application.Commands;
using OTPService.Application.Messages;

namespace OTPService.Infrastructure.Messages;

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
                var message = JsonConvert.DeserializeObject<OtpValidatedMessage>(messageJson);

                if (message is not null)
                {
                    using (var scope = _serviceProvider.CreateAsyncScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new RegisterOtpUserCommand(message.UserId);
                        await mediator.Send(command);    
                    }
                }
                
                break;
            default:
                break;
        }
    }
}