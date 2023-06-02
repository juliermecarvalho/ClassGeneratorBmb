using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.UpdateExampleCommand.v1;

public class UpdateExampleCommandHandler : Handler<UpdateExampleCommand, UpdateExampleCommandResult?> 
{
    private readonly IExampleRepository _exampleRepository;

    public UpdateExampleCommandHandler(INotificationContext notificationContext, 
        IExampleRepository exampleRepository) : base(notificationContext)
    {
        _exampleRepository = exampleRepository;
    }
    
    public override async Task<UpdateExampleCommandResult?> Handle(UpdateExampleCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = await _exampleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            NotificationContext.Push(null, string.Empty, NotificationType.NotFound, 
                request.GetCorrelation());
            
            return null;
        }

        entity.PropertyOne = request.PropertyOne;
        entity.PropertyTwo = request.PropertyTwo;
        
        await _exampleRepository.UpdateAsync(entity, cancellationToken);

        return new UpdateExampleCommandResult
        {
            PropertyOne = entity.PropertyOne,
            PropertyTwo = entity.PropertyTwo,
            Id = entity.Id
        };
    }
}