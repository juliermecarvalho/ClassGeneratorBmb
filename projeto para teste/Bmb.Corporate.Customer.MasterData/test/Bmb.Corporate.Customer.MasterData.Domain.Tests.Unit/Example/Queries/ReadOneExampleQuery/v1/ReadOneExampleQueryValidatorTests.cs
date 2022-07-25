using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQueryValidatorTests
{
    private readonly ReadOneSegmentQueryValidator _subject;

    public ReadOneSegmentQueryValidatorTests()
    {
        _subject = new ReadOneSegmentQueryValidator();
    }
    
    [Fact(DisplayName = "ReadOneSegmentQueryValidator throw invalid query")]
    public void Should_indicate_invalid_query()
    {
        var invalidQuery = new Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1.ReadOneSegmentQuery(0);
        var result = _subject.Validate(invalidQuery);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "ReadOneSegmentQueryValidator validate query successfully")]
    public void Should_validate_query_successfully()
    {
        var validCommand = new Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1.ReadOneSegmentQuery(1);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}