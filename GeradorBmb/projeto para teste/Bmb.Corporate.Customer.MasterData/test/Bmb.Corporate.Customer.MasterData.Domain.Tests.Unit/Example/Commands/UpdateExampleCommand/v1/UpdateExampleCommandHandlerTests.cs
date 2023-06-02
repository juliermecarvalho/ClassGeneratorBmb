using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.UpdateSegmentCommand.v1;

public class UpdateSegmentCommandHandlerTests
{
    private const int SegmentId = 1;
    private readonly Mock<IMapper> _mapperMock;

    private readonly Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1.UpdateSegmentCommand _command;
    private readonly Customer.MasterData.Domain.Segment.Entities.v1.Segment _Segment;
    private readonly UpdateSegmentCommandHandler _subject;
    private readonly INotificationContext _notificationContext;
    private readonly Mock<ISegmentRepository> _SegmentRepositoryMock;

    public UpdateSegmentCommandHandlerTests()
    {
        _command = new()
        {
            Id = 1,
            Name = "julierme carvalho",
            IsActive = true,
            Abbreviations = "jc2",
            UserId = "julierme",
        };

        _Segment = new ("", false, "");
        _mapperMock = new Mock<IMapper>();
        _notificationContext = new NotificationContext();
        _SegmentRepositoryMock = new Mock<ISegmentRepository>();
        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(SegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Segment);
        
        _subject = new UpdateSegmentCommandHandler(_notificationContext, _SegmentRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingSegmentId = 20;

        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(notExistingSegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.Segment.Entities.v1.Segment?)null);

        var result = await _subject.Handle(
            new Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1.UpdateSegmentCommand
            {
                Id = 10,
                Name = "julierme carvalho",
                IsActive = true,
                Abbreviations = "jc2",
                UserId = "julierme",
            }, CancellationToken.None);

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

    [Fact(DisplayName = "Should perform repository call to update Segment")]
    public async Task Should_perform_repository_call_to_update()
    {
        await _subject.Handle(_command, CancellationToken.None);

        _SegmentRepositoryMock.Verify(x => x.UpdateAsync(_Segment, It.IsAny<CancellationToken>()), Times.Once);
    }
}