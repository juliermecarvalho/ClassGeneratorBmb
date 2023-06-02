using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;

public class UpdateSegmentCommandHandler : Handler<UpdateSegmentCommand, UpdateSegmentCommandResult?> 
{
    private readonly IMapper _mapper;

    private readonly ISegmentRepository _segmentRepository;

    public UpdateSegmentCommandHandler(INotificationContext notificationContext, 
        ISegmentRepository SegmentRepository, IMapper mapper) : base(notificationContext)
    {
        _mapper = mapper;
        _segmentRepository = SegmentRepository;
    }

    public override async Task<UpdateSegmentCommandResult?> Handle(UpdateSegmentCommand request,
        CancellationToken cancellationToken)
    {

        var entity = await _segmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            NotificationContext.Push(null, string.Empty, NotificationType.NotFound,
                request.GetCorrelation());

            return null;
        }

        entity.ChangeSegment(request.Name, request.Abbreviations, request.UserId, request.IsActive);

        await _segmentRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<UpdateSegmentCommandResult>(entity);

    }




}



