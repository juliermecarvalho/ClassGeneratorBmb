using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;

public class ReadOneExampleQueryHandler : Handler<ReadOneExampleQuery, ReadOneExampleQueryResult?>
{
    private readonly IExampleRepository _exampleRepository;
    private readonly IMapper _mapper;
    
    public ReadOneExampleQueryHandler(INotificationContext notificationContext, IExampleRepository exampleRepository, 
        IMapper mapper) : base(notificationContext)
    {
        _exampleRepository = exampleRepository;
        _mapper = mapper;
    }

    public override async Task<ReadOneExampleQueryResult?> Handle(ReadOneExampleQuery request, CancellationToken cancellationToken)
    {
        var result = await _exampleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) 
            return _mapper.Map<ReadOneExampleQueryResult>(result);
        
        NotificationContext.Push("Not found", NotificationType.NotFound, request.GetCorrelation());
        return null;
    }
}