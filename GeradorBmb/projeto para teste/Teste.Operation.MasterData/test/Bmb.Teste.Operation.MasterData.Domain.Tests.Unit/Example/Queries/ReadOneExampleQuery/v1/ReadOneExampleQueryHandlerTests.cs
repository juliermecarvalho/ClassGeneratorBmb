using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Queries.ReadOneExampleQuery.v1;

public class ReadOneExampleQueryHandlerTests
{
    private const int ExampleId = 10;
    private readonly ReadOneExampleQueryHandler _subject;
    private readonly Domain.Example.Queries.ReadOneExampleQuery.v1.ReadOneExampleQuery _query;
    private readonly Domain.Example.Entities.v1.Example _example;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<IExampleRepository> _exampleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public ReadOneExampleQueryHandlerTests()
    {
        _query = new Domain.Example.Queries.ReadOneExampleQuery.v1.ReadOneExampleQuery(ExampleId);
        _example = new Domain.Example.Entities.v1.Example
        {
            PropertyOne = "value one",
            PropertyTwo = true
        };

        _notificationContext = new NotificationContext();
        
        _exampleRepositoryMock = new Mock<IExampleRepository>();
        _exampleRepositoryMock.Setup(x => x.GetByIdAsync(ExampleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_example);
        
        _mapperMock = new Mock<IMapper>();

        _subject = new ReadOneExampleQueryHandler(_notificationContext,
            _exampleRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingExampleId = 20;

        _exampleRepositoryMock.Setup(x => x.GetByIdAsync(notExistingExampleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Example.Entities.v1.Example?) null);
        
        var result = await _subject.Handle(new Domain.Example.Queries.ReadOneExampleQuery.v1.ReadOneExampleQuery(notExistingExampleId), CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should map from entity to query result")]
    public async Task Should_map_from_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _mapperMock.Verify(x=>x.Map<ReadOneExampleQueryResult>(_example), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to get example")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _exampleRepositoryMock.Verify(x => x.GetByIdAsync(ExampleId, It.IsAny<CancellationToken>()), Times.Once);
    }
}