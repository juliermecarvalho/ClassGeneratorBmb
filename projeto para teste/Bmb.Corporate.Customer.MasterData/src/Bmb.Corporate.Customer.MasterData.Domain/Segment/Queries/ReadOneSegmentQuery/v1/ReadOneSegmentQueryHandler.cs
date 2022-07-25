using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQueryHandler : Handler<ReadOneSegmentQuery, ReadOneSegmentQueryResult?>
{
    private readonly ISegmentRepository _segmentRepository;
    private readonly IMapper _mapper;
    
    public ReadOneSegmentQueryHandler(INotificationContext notificationContext, ISegmentRepository SegmentRepository, 
        IMapper mapper) : base(notificationContext)
    {
        _segmentRepository = SegmentRepository;
        _mapper = mapper;
    }

    public override async Task<ReadOneSegmentQueryResult?> Handle(ReadOneSegmentQuery request, CancellationToken cancellationToken)
    {
        var result = await _segmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) 
            return _mapper.Map<ReadOneSegmentQueryResult>(result);
        
        NotificationContext.Push("Not found", NotificationType.NotFound, request.GetCorrelation());
        return null;
    }
}