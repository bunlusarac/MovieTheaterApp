using LoyaltyService.Application.DTOs;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Domain.Entities;
using MediatR;

namespace LoyaltyService.Application.Queries;

public class GetCustomerRedeemsQuery: IRequest<IEnumerable<RedeemDto>>
{
    public Guid CustomerId { get; set; }

    public GetCustomerRedeemsQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}

public class GetCustomerRedeemsQueryHandler : IRequestHandler<GetCustomerRedeemsQuery, IEnumerable<RedeemDto>>
{
    private readonly ILoyaltyCustomerRepository _loyaltyCustomerRepository;

    public GetCustomerRedeemsQueryHandler(ILoyaltyCustomerRepository loyaltyCustomerRepository)
    {
        _loyaltyCustomerRepository = loyaltyCustomerRepository;
    }

    public async Task<IEnumerable<RedeemDto>> Handle(GetCustomerRedeemsQuery request, CancellationToken cancellationToken)
    {
        var loyaltyCustomer = await _loyaltyCustomerRepository.GetByCustomerId(request.CustomerId);
        
        return loyaltyCustomer.Redeems.Select(r => new RedeemDto
        {
            CampaignId = r.CampaignId,
            LoyaltyCustomerId = r.LoyaltyCustomerId,
            RedeemDate = r.RedeemDate,
            Transaction = r.Transaction.Amount
        });
    }
}