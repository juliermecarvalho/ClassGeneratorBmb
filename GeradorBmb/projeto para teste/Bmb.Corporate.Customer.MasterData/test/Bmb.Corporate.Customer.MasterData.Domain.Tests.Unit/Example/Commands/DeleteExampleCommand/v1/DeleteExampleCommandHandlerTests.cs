
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.DeleteSegmentCommand.v1;

public class DeleteSegmentCommandHandlerTests
{
    private const int SegmentId = 10;
    
    private readonly DeleteSegmentCommandHandler _subject;
    private readonly Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommand _command;
    private readonly Customer.MasterData.Domain.Segment.Entities.v1.Segment _Segment;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<ISegmentRepository> _SegmentRepositoryMock;

    public DeleteSegmentCommandHandlerTests()
    {
        _command = new Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommand(SegmentId);
        _Segment = new Customer.MasterData.Domain.Segment.Entities.v1.Segment("", false, "");

        _notificationContext = new NotificationContext();
        
        _SegmentRepositoryMock = new Mock<ISegmentRepository>();
        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(SegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Segment);

        _subject = new DeleteSegmentCommandHandler(_notificationContext,
            _SegmentRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingSegmentId = 20;

        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(notExistingSegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.Segment.Entities.v1.Segment ?)null);

        var result = await _subject.Handle(
            new Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommand(notExistingSegmentId),
            CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should perform repository call to get Segment")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _SegmentRepositoryMock.Verify(x => x.GetByIdAsync(SegmentId, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to inactivate Segment")]
    public async Task Should_perform_repository_call_to_inactivate()
    {
        await _subject.Handle(_command, CancellationToken.None);
        
        _SegmentRepositoryMock.Verify(x => x.RemoveAsync(_Segment, It.IsAny<CancellationToken>()), Times.Once);
    }
}