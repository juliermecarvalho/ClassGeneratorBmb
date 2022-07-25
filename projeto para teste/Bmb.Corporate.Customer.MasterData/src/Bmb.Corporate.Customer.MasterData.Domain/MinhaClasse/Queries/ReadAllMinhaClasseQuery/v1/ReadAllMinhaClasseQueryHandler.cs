using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;

public class ReadAllMinhaClasseQueryHandler : Handler<ReadAllMinhaClasseQuery, IList<ReadAllMinhaClasseQueryResult>>
{
    private readonly IMapper _mapper;
    private readonly IMinhaClasseRepository _minhaclasseRepository;
    
    public ReadAllMinhaClasseQueryHandler(INotificationContext notificationContext, IMapper mapper, 
        IMinhaClasseRepository MinhaClasseRepository) : base(notificationContext)
    {
            _mapper = mapper;
            _minhaclasseRepository = MinhaClasseRepository;
    }

    public override async Task<IList<ReadAllMinhaClasseQueryResult>> Handle(ReadAllMinhaClasseQuery request, 
        CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<ReadAllMinhaClasseQueryResult>>(await _minhaclasseRepository.ReadAll(request, cancellationToken));        

    }
}
