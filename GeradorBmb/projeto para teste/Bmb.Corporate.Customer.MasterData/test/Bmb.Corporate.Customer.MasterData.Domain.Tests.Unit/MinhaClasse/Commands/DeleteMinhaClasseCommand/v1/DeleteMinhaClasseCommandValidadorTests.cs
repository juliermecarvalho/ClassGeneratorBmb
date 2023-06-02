using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;

public class DeleteMinhaClasseCommandValidatorTests
{
    private readonly Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommandValidator _subject;

    public DeleteMinhaClasseCommandValidatorTests()
    {
        _subject = new ();
    }
    
    [Fact(DisplayName = "DeleteMinhaClasseCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommand(0);
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "DeleteMinhaClasseCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1.DeleteMinhaClasseCommand(10);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
