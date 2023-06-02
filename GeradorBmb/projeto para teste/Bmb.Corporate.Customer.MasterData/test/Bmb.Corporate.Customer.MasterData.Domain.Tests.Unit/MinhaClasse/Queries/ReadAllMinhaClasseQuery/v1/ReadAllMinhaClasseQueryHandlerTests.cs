using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;

public class ReadAllMinhaClasseQueryHandlerTests
{
    private readonly ReadAllMinhaClasseQueryHandler _subject;
    private readonly IList<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse> _minhaclasses;
    private readonly Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1.ReadAllMinhaClasseQuery _query;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMinhaClasseRepository> _minhaclasseRepositoryMock;
    

    public ReadAllMinhaClasseQueryHandlerTests()
    {
        _query = new ( true);

        _minhaclasses = new List<Customer.MasterData.Domain.MinhaClasse.Entities.v1.MinhaClasse>();
            
        _minhaclasseRepositoryMock = new Mock<IMinhaClasseRepository>();
        _minhaclasseRepositoryMock.Setup(x => x.ReadAll(_query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_minhaclasses);

        _mapperMock = new Mock<IMapper>();
        
        _subject = new ReadAllMinhaClasseQueryHandler(Mock.Of<INotificationContext>(), _mapperMock.Object, 
            _minhaclasseRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should perform repository call to get all MinhaClasses")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _minhaclasseRepositoryMock.Verify(x => x.ReadAll(_query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should map from database entity to query result")]
    public async Task Should_map_from_database_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);

        _mapperMock.Verify(x => x.Map<IList<ReadAllMinhaClasseQueryResult>>(_minhaclasses), Times.Once);
    }
}
