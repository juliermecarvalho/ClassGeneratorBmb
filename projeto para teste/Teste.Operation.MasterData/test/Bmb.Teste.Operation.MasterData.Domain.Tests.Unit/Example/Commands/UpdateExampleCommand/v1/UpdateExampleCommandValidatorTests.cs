using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.UpdateExampleCommand.v1;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Commands.UpdateExampleCommand.v1;

public class UpdateExampleCommandValidatorTests
{
    private readonly UpdateExampleCommandValidator _subject;

    public UpdateExampleCommandValidatorTests()
    {
        _subject = new UpdateExampleCommandValidator();
    }
    
    [Fact(DisplayName = "UpdateExampleCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Domain.Example.Commands.UpdateExampleCommand.v1.UpdateExampleCommand
        {
            Id = 1,
            PropertyOne = null,
            PropertyTwo = true
        };
        
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "UpdateExampleCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Domain.Example.Commands.UpdateExampleCommand.v1.UpdateExampleCommand
        {
            Id = 1,
            PropertyOne = "Value one",
            PropertyTwo = true
        };
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}