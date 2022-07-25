using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseQueryHandler : Handler<ReadOneMinhaClasseQuery, ReadOneMinhaClasseQueryResult?>
{
    private readonly IMapper _mapper;
    private readonly IMinhaClasseRepository _minhaclasseRepository;
    
    public ReadOneMinhaClasseQueryHandler(INotificationContext notificationContext, IMapper mapper, 
        IMinhaClasseRepository MinhaClasseRepository) : base(notificationContext)
    {
            _mapper = mapper;
            _minhaclasseRepository = MinhaClasseRepository;
    }

    public override async Task<ReadOneMinhaClasseQueryResult?> Handle(ReadOneMinhaClasseQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _minhaclasseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) 
            return _mapper.Map<ReadOneMinhaClasseQueryResult>(result);
        
        NotificationContext.Push("Not found", NotificationType.NotFound, request.GetCorrelation());
        return null;

    }
}
