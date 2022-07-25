using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Core.Domain.Models;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1;

public class DeleteSegmentCommandHandler : Handler<DeleteSegmentCommand, EmptyResult?>
{
    private readonly ISegmentRepository _segmentRepository;

    public DeleteSegmentCommandHandler(INotificationContext notificationContext, ISegmentRepository SegmentRepository) : 
        base(notificationContext)
    {
        _segmentRepository = SegmentRepository;
    }
    
    public override async Task<EmptyResult?> Handle(DeleteSegmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _segmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            NotificationContext.Push(null, "Not found", NotificationType.NotFound, request.GetCorrelation());
            return null;
        }
        
        await _segmentRepository.RemoveAsync(entity, cancellationToken);

        return new EmptyResult();
    }
}