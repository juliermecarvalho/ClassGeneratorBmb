using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseQueryHandlerTests
{
    private const int MinhaClasseId = 10;
    private readonly ReadOneMinhaClasseQueryHandler _subject;
    private readonly Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1.ReadOneMinhaClasseQuery _query;
    private readonly Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse _minhaclasse;

    private readonly INotificationContext _notificationContext;
    private readonly Mock<IMinhaClasseRepository> _minhaclasseRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public ReadOneMinhaClasseQueryHandlerTests()
    {
        _query = new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1.ReadOneMinhaClasseQuery(MinhaClasseId);
        _minhaclasse = new();

        _notificationContext = new NotificationContext();
        
        _minhaclasseRepositoryMock = new Mock<IMinhaClasseRepository>();
        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(MinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_minhaclasse);
        
        _mapperMock = new Mock<IMapper>();

        _subject = new ReadOneMinhaClasseQueryHandler(_notificationContext, _mapperMock.Object,
            _minhaclasseRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should set NotificationContext with NotFound type when not found entity")]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {
        const int notExistingMinhaClasseId = 20;

        _minhaclasseRepositoryMock.Setup(x => x.GetByIdAsync(notExistingMinhaClasseId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse?)null);

        var result = await _subject.Handle(new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1.ReadOneMinhaClasseQuery(notExistingMinhaClasseId), CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    }

    [Fact(DisplayName = "Should map from entity to query result")]
    public async Task Should_map_from_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _mapperMock.Verify(x=>x.Map<ReadOneMinhaClasseQueryResult>(_minhaclasse), Times.Once);
    }
    
    [Fact(DisplayName = "Should perform repository call to get MinhaClasse")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _minhaclasseRepositoryMock.Verify(x => x.GetByIdAsync(MinhaClasseId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
