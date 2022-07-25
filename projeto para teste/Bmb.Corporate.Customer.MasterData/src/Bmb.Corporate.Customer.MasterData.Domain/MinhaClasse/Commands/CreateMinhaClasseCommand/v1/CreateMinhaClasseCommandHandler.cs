using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommandHandler : Handler<CreateMinhaClasseCommand, CreateMinhaClasseCommandResult>
{
    private readonly IMapper _mapper;
    private readonly IMinhaClasseRepository _minhaclasseRepository;
    
    public CreateMinhaClasseCommandHandler(INotificationContext notificationContext, IMapper mapper, 
        IMinhaClasseRepository MinhaClasseRepository) : base(notificationContext)
    {
            _mapper = mapper;
            _minhaclasseRepository = MinhaClasseRepository;
    }

    public override async Task<CreateMinhaClasseCommandResult> Handle(CreateMinhaClasseCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.v1.MinhaClasse>(request);
        var id = await _minhaclasseRepository.AddAsync(entity, cancellationToken);
    
        return return _mapper.Map<CreateMinhaClasseCommandResult>(entity);
    }
}
