using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Exceptions;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
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
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;

    public RegisterLoyaltyCustomerCommandHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
    }

    public async Task<LoyaltyCustomerCreatedDto> Handle(RegisterLoyaltyCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
            throw new LoyaltyApplicationException(LoyaltyApplicationErrorCode.UserAlreadyExists);
        }
        catch (Exception e)
        {
            var loyaltyCustomer = new LoyaltyCustomer(request.CustomerId);
            await _loyaltyCustomerRepository.Add(loyaltyCustomer);
            return new LoyaltyCustomerCreatedDto
            {
                LoyaltyCustomerId = loyaltyCustomer.Id
            };
        }
    }
}