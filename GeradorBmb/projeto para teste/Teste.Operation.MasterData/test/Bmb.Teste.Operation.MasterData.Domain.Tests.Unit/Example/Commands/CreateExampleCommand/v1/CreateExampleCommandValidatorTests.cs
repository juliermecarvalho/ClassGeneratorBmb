using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommandValidatorTests
{
    private const string PropertyOneValue = "PropertyOneValue";
    private const bool PropertyTwoValue = true;
    
    private readonly CreateExampleCommandValidator _subject;

    public CreateExampleCommandValidatorTests()
    {
        _subject = new CreateExampleCommandValidator();
    }

    [Fact(DisplayName = "CreateExampleCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Domain.Example.Commands.CreateExampleCommand.v1.CreateExampleCommand();
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "CreateExampleCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Domain.Example.Commands.CreateExampleCommand.v1.CreateExampleCommand
        {
            PropertyOne = PropertyOneValue,
            PropertyTwo = PropertyTwoValue
        };
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}