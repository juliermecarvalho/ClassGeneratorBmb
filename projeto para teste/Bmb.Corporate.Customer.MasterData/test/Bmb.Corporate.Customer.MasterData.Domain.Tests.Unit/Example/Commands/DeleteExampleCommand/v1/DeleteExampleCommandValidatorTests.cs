using Xunit;

namespace Bmb.Corporate.Segmentr.MasterData.Domain.Tests.Unit.Segment.Commands.DeleteSegmentCommand.v1;

public class DeleteSegmentCommandValidatorTests
{
    private readonly Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommandValidator _subject;

    public DeleteSegmentCommandValidatorTests()
    {
        _subject = new ();
    }
    
    [Fact(DisplayName = "DeleteSegmentCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommand(0);
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "DeleteSegmentCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1.DeleteSegmentCommand(10);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}