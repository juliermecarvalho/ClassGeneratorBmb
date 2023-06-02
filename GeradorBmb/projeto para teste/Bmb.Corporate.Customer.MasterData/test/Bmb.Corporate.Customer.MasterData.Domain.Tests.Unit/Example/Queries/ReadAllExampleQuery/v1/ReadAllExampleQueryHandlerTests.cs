using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Queries.ReadAllSegmentQuery.v1;

public class ReadAllSegmentQueryHandlerTests
{
    private readonly ReadAllSegmentQueryHandler _subject;
    private readonly IList<Customer.MasterData.Domain.Segment.Entities.v1.Segment> _Segments;
    private readonly Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1.ReadAllSegmentQuery _query;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ISegmentRepository> _SegmentRepositoryMock;
    

    public ReadAllSegmentQueryHandlerTests()
    {
        _query = new ( true);

        _Segments = new List<Customer.MasterData.Domain.Segment.Entities.v1.Segment>();
            
        _SegmentRepositoryMock = new Mock<ISegmentRepository>();
        _SegmentRepositoryMock.Setup(x => x.ReadAll(_query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_Segments);

        _mapperMock = new Mock<IMapper>();
        
        _subject = new ReadAllSegmentQueryHandler(Mock.Of<INotificationContext>(), _SegmentRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact(DisplayName = "Should perform repository call to get all Segments")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _SegmentRepositoryMock.Verify(x => x.ReadAll(_query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should map from database entity to query result")]
    public async Task Should_map_from_database_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);

        _mapperMock.Verify(x => x.Map<IList<ReadAllSegmentQueryResult>>(_Segments), Times.Once);
    }
}