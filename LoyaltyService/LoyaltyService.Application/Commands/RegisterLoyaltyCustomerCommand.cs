using LoyaltyService.Application.DTOs;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class RegisterLoyaltyCustomerCommand: IRequest<LoyaltyCustomerCreatedDto>
{
    public Guid CustomerId { get; set; }

    public RegisterLoyaltyCustomerCommand(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public class
    RegisterLoyaltyCustomerCommandHandler : IRequestHandler<RegisterLoyaltyCustomerCommand, LoyaltyCustomerCreatedDto>
{
    public Task<LoyaltyCustomerCreatedDto> Handle(RegisterLoyaltyCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}