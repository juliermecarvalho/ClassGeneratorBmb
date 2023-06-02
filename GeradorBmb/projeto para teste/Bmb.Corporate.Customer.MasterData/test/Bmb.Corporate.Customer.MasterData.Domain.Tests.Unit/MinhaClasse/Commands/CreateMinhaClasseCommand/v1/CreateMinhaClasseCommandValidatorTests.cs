using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

using Xunit;

namespace Bmb.Corporate.Customer.MasterData.Domain.Tests.Unit.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommandValidatorTests
{
   
    
    private readonly CreateMinhaClasseCommandValidator _subject;

    public CreateMinhaClasseCommandValidatorTests()
    {
        _subject = new CreateMinhaClasseCommandValidator();
    }

    [Fact(DisplayName = "CreateMinhaClasseCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1.CreateMinhaClasseCommand();
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "CreateMinhaClasseCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1.CreateMinhaClasseCommand
        {
           
        };
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
