using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommandHandler : Handler<UpdateMinhaClasseCommand, UpdateMinhaClasseCommandResult>
{
    private readonly IMapper _mapper;
    private readonly IMinhaClasseRepository _minhaclasseRepository;
    
    public UpdateMinhaClasseCommandHandler(INotificationContext notificationContext, IMapper mapper, 
        IMinhaClasseRepository MinhaClasseRepository) : base(notificationContext)
    {
            _mapper = mapper;
            _minhaclasseRepository = MinhaClasseRepository;
    }

    public override async Task<UpdateMinhaClasseCommandResult?> Handle(UpdateMinhaClasseCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = await _minhaclasseRepository.GetByIdAsync(request.Id, cancellationToken);
      
        if (entity == null)
        {
            NotificationContext.Push(null, string.Empty, NotificationType.NotFound,
                request.GetCorrelation());

            return null;
        }

        entity.ChangeMinhaClasse(request.Idade, request.Nome);

        await _minhaclasseRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<UpdateMinhaClasseCommandResult>(entity);


    }
}
