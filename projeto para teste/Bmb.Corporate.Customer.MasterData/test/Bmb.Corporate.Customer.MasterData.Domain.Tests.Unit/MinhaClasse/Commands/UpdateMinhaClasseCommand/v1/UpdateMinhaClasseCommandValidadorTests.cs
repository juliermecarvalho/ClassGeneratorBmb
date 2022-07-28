using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;
using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommandValidatorTests
{
    private readonly UpdateMinhaClasseCommandValidator _subject;

    public UpdateMinhaClasseCommandValidatorTests()
    {
        _subject = new UpdateMinhaClasseCommandValidator();
    }
    
    [Fact(DisplayName = "UpdateMinhaClasseCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1.UpdateMinhaClasseCommand
        {
           
        };
        
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "UpdateMinhaClasseCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1.UpdateMinhaClasseCommand
        {
           
        };
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
