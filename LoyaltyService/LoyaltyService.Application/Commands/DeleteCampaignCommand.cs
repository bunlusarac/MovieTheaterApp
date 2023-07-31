using LoyaltyService.Application.Persistence;
using MediatR;

namespace LoyaltyService.Application.Commands;

public class DeleteCampaignCommand: IRequest
{
    public Guid CampaignId { get; set; }

    public DeleteCampaignCommand(Guid campaignId)
    {
        CampaignId = campaignId;
    }
}

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand>
{
    private readonly ICampaignRepository _campaignRepository;

    public DeleteCampaignCommandHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        await _campaignRepository.DeleteById(request.CampaignId);
    }
}