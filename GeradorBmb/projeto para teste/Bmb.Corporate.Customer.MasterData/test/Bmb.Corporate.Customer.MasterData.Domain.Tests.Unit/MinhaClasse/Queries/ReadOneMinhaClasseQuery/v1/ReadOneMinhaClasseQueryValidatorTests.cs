using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseQueryValidatorTests
{
    private readonly ReadOneMinhaClasseQueryValidator _subject;

    public ReadOneMinhaClasseQueryValidatorTests()
    {
        _subject = new ReadOneMinhaClasseQueryValidator();
    }
    
    [Fact(DisplayName = "ReadOneMinhaClasseQueryValidator throw invalid query")]
    public void Should_indicate_invalid_query()
    {
        var invalidQuery = new Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1.ReadOneMinhaClasseQuery(0);
        var result = _subject.Validate(invalidQuery);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "ReadOneMinhaClasseQueryValidator validate query successfully")]
    public void Should_validate_query_successfully()
    {
        var validCommand = new Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1.ReadOneMinhaClasseQuery(1);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
