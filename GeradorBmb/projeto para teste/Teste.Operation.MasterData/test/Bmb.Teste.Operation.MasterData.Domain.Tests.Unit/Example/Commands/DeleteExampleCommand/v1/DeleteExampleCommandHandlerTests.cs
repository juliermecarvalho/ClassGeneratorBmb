using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Commands.DeleteExampleCommand.v1;

public class DeleteExampleCommandHandlerTests
{
    private const int ExampleId = 10;
    
    private readonly DeleteExampleCommandHandler _subject;
    private readonly Domain.Example.Commands.DeleteExampleCommand.v1.DeleteExampleCommand _command;
    private readonly Domain.Example.Entities.v1.Example _example;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<IExampleRepository> _exampleRepositoryMock;

    public DeleteExampleCommandHandlerTests()
    {
        _command = new Domain.Example.Commands.DeleteExampleCommand.v1.DeleteExampleCommand(ExampleId);
        _example = new Domain.Example.Entities.v1.Example
        {
            PropertyOne = "value one",
            PropertyTwo = true
        };

        _notificationContext = new NotificationContext();
        
        _exampleRepositoryMock = new Mock<IExampleRepository>();
        _exampleRepositoryMock.Setup(x => x.GetByIdAsync(ExampleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_example);

        _subject = new DeleteExampleCommandHandler(_notificationContext,
            _exampleRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingExampleId = 20;

        _exampleRepositoryMock.Setup(x => x.GetByIdAsync(notExistingExampleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Example.Entities.v1.Example?) null);
        
        var result = await _subject.Handle(
            new Domain.Example.Commands.DeleteExampleCommand.v1.DeleteExampleCommand(notExistingExampleId), 
            CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should perform repository call to get example")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _exampleRepositoryMock.Verify(x => x.GetByIdAsync(ExampleId, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to inactivate example")]
    public async Task Should_perform_repository_call_to_inactivate()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _exampleRepositoryMock.Verify(x => x.RemoveAsync(_example, It.IsAny<CancellationToken>()), Times.Once);
    }
}