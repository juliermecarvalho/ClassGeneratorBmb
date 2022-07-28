using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;

public class DeleteMinhaClasseCommandHandlerTests
{
    private const int MinhaClasseId = 10;
    
    private readonly DeleteMinhaClasseCommandHandler _subject;
    private readonly Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommand _command;
    private readonly Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse _minhaclasse;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<IMinhaClasseRepository> _minhaclasseRepositoryMock;

    public DeleteMinhaClasseCommandHandlerTests()
    {
        _command = new Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommand(MinhaClasseId);
        _minhaclasse = new Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse(); /*verificar os contrutores.*/

        _notificationContext = new NotificationContext();
        
        _minhaclasseRepositoryMock = new Mock<IMinhaClasseRepository>();
        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(MinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_minhaclasse);

        _subject = new DeleteMinhaClasseCommandHandler(_notificationContext,
            _minhaclasseRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingMinhaClasseId = 20;

        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(notExistingMinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse ?)null);

        var result = await _subject.Handle(
            new Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommand(notExistingMinhaClasseId),
            CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should perform repository call to get MinhaClasse")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _minhaclasseRepositoryMock.Verify(x => x.GetByIdAsync(MinhaClasseId, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to inactivate MinhaClasse")]
    public async Task Should_perform_repository_call_to_inactivate()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _minhaclasseRepositoryMock.Verify(x => x.RemoveAsync(_minhaclasse, It.IsAny<CancellationToken>()), Times.Once);
    }
}
