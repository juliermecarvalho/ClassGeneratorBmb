using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommandHandlerTests
{
    private const int MinhaClasseId = 1;
    private readonly Mock<IMapper> _mapperMock;

    private readonly Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1.UpdateMinhaClasseCommand _command;
    private readonly Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse _minhaclasse;
    private readonly UpdateMinhaClasseCommandHandler _subject;
    private readonly INotificationContext _notificationContext;
    private readonly Mock<IMinhaClasseRepository> _minhaclasseRepositoryMock;

    public UpdateMinhaClasseCommandHandlerTests()
    {
        _command = new()
        {
          
        };

        _minhaclasse = new ();
        _mapperMock = new Mock<IMapper>();
        _notificationContext = new NotificationContext();
        _minhaclasseRepositoryMock = new Mock<IMinhaClasseRepository>();
        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(MinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_minhaclasse);
        
        _subject = new UpdateMinhaClasseCommandHandler(_notificationContext, _mapperMock.Object,_minhaclasseRepositoryMock.Object);
    }
    
    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingMinhaClasseId = 20;

        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(notExistingMinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse?)null);

        var result = await _subject.Handle(
            new Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1.UpdateMinhaClasseCommand
            {
              
            }, CancellationToken.None);

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

    [Fact(DisplayName = "Should perform repository call to update MinhaClasse")]
    public async Task Should_perform_repository_call_to_update()
    {
        await _subject.Handle(_command, CancellationToken.None);

        _minhaclasseRepositoryMock.Verify(x => x.UpdateAsync(_minhaclasse, It.IsAny<CancellationToken>()), Times.Once);
    }
}
