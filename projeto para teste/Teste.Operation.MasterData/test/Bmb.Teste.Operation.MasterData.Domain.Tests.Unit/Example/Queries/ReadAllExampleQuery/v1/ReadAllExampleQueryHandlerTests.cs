using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;
using Moq;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Queries.ReadAllExampleQuery.v1;

public class ReadAllExampleQueryHandlerTests
{
    private readonly ReadAllExampleQueryHandler _subject;
    private readonly IList<Domain.Example.Entities.v1.Example> _examples;
    private readonly Domain.Example.Queries.ReadAllExampleQuery.v1.ReadAllExampleQuery _query;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IExampleRepository> _exampleRepositoryMock;
    

    public ReadAllExampleQueryHandlerTests()
    {
        _query = new Domain.Example.Queries.ReadAllExampleQuery.v1.ReadAllExampleQuery(null, null, true);
        
        _examples = new List<Domain.Example.Entities.v1.Example>
        {
            new() { PropertyOne = "value one", PropertyTwo = true },
            new() { PropertyOne = "value two", PropertyTwo = true },
            new() { PropertyOne = "value three", PropertyTwo = false }
        };
            
        _exampleRepositoryMock = new Mock<IExampleRepository>();
        _exampleRepositoryMock.Setup(x => x.ReadAll(_query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_examples);

        _mapperMock = new Mock<IMapper>();
        
        _subject = new ReadAllExampleQueryHandler(Mock.Of<INotificationContext>(), _exampleRepositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact(DisplayName = "Should perform repository call to get all examples")]
    public async Task Should_perform_repository_call()
    {
        await _subject.Handle(_query, CancellationToken.None);
        
        _exampleRepositoryMock.Verify(x => x.ReadAll(_query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Should map from database entity to query result")]
    public async Task Should_map_from_database_entity_to_query_result()
    {
        await _subject.Handle(_query, CancellationToken.None);

        _mapperMock.Verify(x => x.Map<IList<ReadAllExampleQueryResult>>(_examples), Times.Once);
    }
}