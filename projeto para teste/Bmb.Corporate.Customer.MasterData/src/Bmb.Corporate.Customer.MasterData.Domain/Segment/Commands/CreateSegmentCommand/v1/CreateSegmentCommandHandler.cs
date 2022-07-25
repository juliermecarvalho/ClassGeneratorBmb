using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandHandler : Handler<CreateSegmentCommand, CreateSegmentCommandResult>
{
    private readonly IMapper _mapper;
    private readonly ISegmentRepository _segmentRepository;
    
    public CreateSegmentCommandHandler(INotificationContext notificationContext, IMapper mapper, 
        ISegmentRepository SegmentRepository) : base(notificationContext)
    {
        _mapper = mapper;
        _segmentRepository = SegmentRepository;
    }

    public override async Task<CreateSegmentCommandResult> Handle(CreateSegmentCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.v1.Segment>(request);
        var id = await _segmentRepository.AddAsync(entity, cancellationToken);

        return _mapper.Map<CreateSegmentCommandResult>(entity);
    }
}