using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;
using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.UpdateSegmentCommand.v1;

public class UpdateSegmentCommandValidatorTests
{
    private readonly UpdateSegmentCommandValidator _subject;

    public UpdateSegmentCommandValidatorTests()
    {
        _subject = new UpdateSegmentCommandValidator();
    }
    
    [Fact(DisplayName = "UpdateSegmentCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1.UpdateSegmentCommand
        {
            Name = "julierme carvalho",
            IsActive = true,
            Abbreviations = "jc2hhhh",
            UserId = "julierme",
        };
        
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "UpdateSegmentCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1.UpdateSegmentCommand
        {
            Id = 1,
            Name = "julierme carvalho",
            IsActive = true,
            Abbreviations = "jc2",
            UserId = "julierme",
        };
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}