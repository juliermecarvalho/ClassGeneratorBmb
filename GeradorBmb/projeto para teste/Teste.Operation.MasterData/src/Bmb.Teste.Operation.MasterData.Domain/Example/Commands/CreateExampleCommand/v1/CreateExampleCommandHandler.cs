using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommandHandler : Handler<CreateExampleCommand, CreateExampleCommandResult>
{
    private readonly IMapper _mapper;
    private readonly IExampleRepository _exampleRepository;
    
    public CreateExampleCommandHandler(INotificationContext notificationContext, IMapper mapper, 
        IExampleRepository exampleRepository) : base(notificationContext)
    {
        _mapper = mapper;
        _exampleRepository = exampleRepository;
    }

    public override async Task<CreateExampleCommandResult> Handle(CreateExampleCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Entities.v1.Example>(request);
        var id = await _exampleRepository.AddAsync(entity, cancellationToken);

        return new CreateExampleCommandResult
        {
            Id = id,
            PropertyOne = entity.PropertyOne,
            PropertyTwo = entity.PropertyTwo
        };
    }
}