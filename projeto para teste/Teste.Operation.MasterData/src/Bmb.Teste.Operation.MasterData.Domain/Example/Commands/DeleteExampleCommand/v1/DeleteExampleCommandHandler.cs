using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Core.Domain.Models;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;

public class DeleteExampleCommandHandler : Handler<DeleteExampleCommand, EmptyResult?>
{
    private readonly IExampleRepository _exampleRepository;

    public DeleteExampleCommandHandler(INotificationContext notificationContext, IExampleRepository exampleRepository) : 
        base(notificationContext)
    {
        _exampleRepository = exampleRepository;
    }
    
    public override async Task<EmptyResult?> Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _exampleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            NotificationContext.Push(null, "Not found", NotificationType.NotFound, request.GetCorrelation());
            return null;
        }
        
        await _exampleRepository.RemoveAsync(entity, cancellationToken);

        return new EmptyResult();
    }
}