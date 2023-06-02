using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQueryHandlerTests
{
    private const int SegmentId = 10;
    private readonly ReadOneSegmentQueryHandler _subject;
    private readonly Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1.ReadOneSegmentQuery _query;
    private readonly Customer.MasterData.Domain.Segment.Entities.v1.Segment _Segment;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<ISegmentRepository> _SegmentRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public ReadOneSegmentQueryHandlerTests()
    {
        _query = new Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1.ReadOneSegmentQuery(SegmentId);
        _Segment = new("", false, "");

        _notificationContext = new NotificationContext();
        
        _SegmentRepositoryMock = new Mock<ISegmentRepository>();
        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(SegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Segment);
        
        _mapperMock = new Mock<IMapper>();

        _subject = new ReadOneSegmentQueryHandler(_notificationContext,
            _SegmentRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingSegmentId = 20;

        _SegmentRepositoryMock.Setup(x => x.GetByIdAsync(notExistingSegmentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.Segment.Entities.v1.Segment?)null);

        var result = await _subject.Handle(new Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1.ReadOneSegmentQuery(notExistingSegmentId), CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should map from entity to query result")]
    public async Task Should_map_from_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _mapperMock.Verify(x=>x.Map<ReadOneSegmentQueryResult>(_Segment), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to get Segment")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _SegmentRepositoryMock.Verify(x => x.GetByIdAsync(SegmentId, It.IsAny<CancellationToken>()), Times.Once);
    }
}