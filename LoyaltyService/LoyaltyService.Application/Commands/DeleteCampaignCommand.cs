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
    public Task Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}