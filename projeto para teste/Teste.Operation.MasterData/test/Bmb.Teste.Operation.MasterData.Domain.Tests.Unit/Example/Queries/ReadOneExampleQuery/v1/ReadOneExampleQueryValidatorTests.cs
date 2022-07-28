using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Queries.ReadOneExampleQuery.v1;

public class ReadOneExampleQueryValidatorTests
{
    private readonly ReadOneExampleQueryValidator _subject;

    public ReadOneExampleQueryValidatorTests()
    {
        _subject = new ReadOneExampleQueryValidator();
    }
    
    [Fact(DisplayName = "ReadOneExampleQueryValidator throw invalid query")]
    public void Should_indicate_invalid_query()
    {
        var invalidQuery = new Domain.Example.Queries.ReadOneExampleQuery.v1.ReadOneExampleQuery(0);
        var result = _subject.Validate(invalidQuery);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "ReadOneExampleQueryValidator validate query successfully")]
    public void Should_validate_query_successfully()
    {
        var validCommand = new Domain.Example.Queries.ReadOneExampleQuery.v1.ReadOneExampleQuery(1);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}