using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Core.Domain.Models;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;

public class DeleteMinhaClasseCommandHandler : Handler<DeleteMinhaClasseCommand, EmptyResult?>
{

    private readonly IMinhaClasseRepository _minhaclasseRepository;
    
    public DeleteMinhaClasseCommandHandler(INotificationContext notificationContext, IMinhaClasseRepository MinhaClasseRepository) : 
        base(notificationContext)
    {

            _minhaclasseRepository = MinhaClasseRepository;
    }

    public override async Task<EmptyResult?> Handle(DeleteMinhaClasseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _minhaclasseRepository.GetByIdAsync(request.Id, cancellationToken);
      
        if (entity == null)
        {
            NotificationContext.Push(null, "Not found", NotificationType.NotFound, request.GetCorrelation());
            return null;
        }


        await _minhaclasseRepository.RemoveAsync(entity, cancellationToken);

        return new EmptyResult();


    }
}
