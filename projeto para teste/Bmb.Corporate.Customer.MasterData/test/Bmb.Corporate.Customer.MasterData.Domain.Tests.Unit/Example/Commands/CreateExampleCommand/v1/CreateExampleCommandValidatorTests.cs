using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;

using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandValidatorTests
{
   
    
    private readonly CreateSegmentCommandValidator _subject;

    public CreateSegmentCommandValidatorTests()
    {
        _subject = new CreateSegmentCommandValidator();
    }

    [Fact(DisplayName = "CreateSegmentCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1.CreateSegmentCommand();
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "CreateSegmentCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1.CreateSegmentCommand
        {
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