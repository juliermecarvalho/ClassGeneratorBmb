using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;

public class ReadAllSegmentQueryHandler : Handler<ReadAllSegmentQuery, IList<ReadAllSegmentQueryResult>>
{
    private readonly ISegmentRepository _segmentRepository;
    private readonly IMapper _mapper;

    public ReadAllSegmentQueryHandler(INotificationContext notificationContext, ISegmentRepository SegmentRepository,
        IMapper mapper) :
        base(notificationContext)
    {
        _mapper = mapper;
        _segmentRepository = SegmentRepository;
    }

    public override async Task<IList<ReadAllSegmentQueryResult>> Handle(ReadAllSegmentQuery request, 
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<ReadAllSegmentQueryResult>>(await _segmentRepository.ReadAll(request, cancellationToken));
    }
}