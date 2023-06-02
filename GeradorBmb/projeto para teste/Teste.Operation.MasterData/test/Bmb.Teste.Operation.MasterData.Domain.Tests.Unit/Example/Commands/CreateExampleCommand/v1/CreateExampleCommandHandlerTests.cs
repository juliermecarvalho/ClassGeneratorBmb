using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommandHandlerTests
{
    private const int ExampleId = 10;
    private readonly CreateExampleCommandHandler _subject;
    private readonly Domain.Example.Commands.CreateExampleCommand.v1.CreateExampleCommand _command;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IExampleRepository> _exampleRepositoryMock;
    
    public CreateExampleCommandHandlerTests()
    {
        _command = new Domain.Example.Commands.CreateExampleCommand.v1.CreateExampleCommand
        {
            PropertyOne = "property one",
            PropertyTwo = true
        };
        
        _exampleRepositoryMock = new Mock<IExampleRepository>();
        _mapperMock = new Mock<IMapper>();

        _mapperMock.Setup(x =>
                x.Map<Domain.Example.Entities.v1.Example>(It.IsAny<Domain.Example.Commands.CreateExampleCommand.v1.CreateExampleCommand>()))
            .Returns(new Domain.Example.Entities.v1.Example { PropertyOne = "Value one", PropertyTwo = true } );

        _exampleRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Domain.Example.Entities.v1.Example>(), 
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(ExampleId));
        
        _subject = new CreateExampleCommandHandler(Mock.Of<INotificationContext>(),
            _mapperMock.Object, _exampleRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should map from command to entity")]
    public async Task Should_map_from_command_to_entity()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _mapperMock.Verify(x => x.Map<Domain.Example.Entities.v1.Example>(_command), Times.Once);
    }

    [Fact(DisplayName = "Should add entity into repository")]
    public async Task Should_add_entity_into_repository()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _exampleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Example.Entities.v1.Example>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}