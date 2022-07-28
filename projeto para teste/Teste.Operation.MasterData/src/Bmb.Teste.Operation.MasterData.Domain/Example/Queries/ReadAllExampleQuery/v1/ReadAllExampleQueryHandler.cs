using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;

public class ReadAllExampleQueryHandler : Handler<ReadAllExampleQuery, IList<ReadAllExampleQueryResult>>
{
    private readonly IExampleRepository _exampleRepository;
    private readonly IMapper _mapper;

    public ReadAllExampleQueryHandler(INotificationContext notificationContext, IExampleRepository exampleRepository,
        IMapper mapper) :
        base(notificationContext)
    {
        _mapper = mapper;
        _exampleRepository = exampleRepository;
    }

    public override async Task<IList<ReadAllExampleQueryResult>> Handle(ReadAllExampleQuery request, 
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<ReadAllExampleQueryResult>>(await _exampleRepository.ReadAll(request, cancellationToken));
    }
}