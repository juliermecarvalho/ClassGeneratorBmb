using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommandHandlerTests
{
    private const int MinhaClasseId = 10;
    private readonly CreateMinhaClasseCommandHandler _subject;
    private readonly Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1.CreateMinhaClasseCommand _command;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMinhaClasseRepository> _minhaclasseRepositoryMock;

    public CreateMinhaClasseCommandHandlerTests()
    {
        _command = new Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1.CreateMinhaClasseCommand
        {
           
        };
        
        _minhaclasseRepositoryMock = new Mock<IMinhaClasseRepository>();
        _mapperMock = new Mock<IMapper>();

        _mapperMock.Setup(x =>
                x.Map<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse>(It.IsAny<Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1.CreateMinhaClasseCommand>()))
            .Returns(new Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse { });

        _minhaclasseRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse>(), 
                It.IsAny<CancellationToken>())).Returns(Task.FromResult(MinhaClasseId));
        
        _subject = new CreateMinhaClasseCommandHandler(Mock.Of<INotificationContext>(),
            _mapperMock.Object, _minhaclasseRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should map from command to entity")]
    public async Task Should_map_from_command_to_entity()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _mapperMock.Verify(x => x.Map<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse>(_command), Times.Once);
    }

    [Fact(DisplayName = "Should add entity into repository")]
    public async Task Should_add_entity_into_repository()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _minhaclasseRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
