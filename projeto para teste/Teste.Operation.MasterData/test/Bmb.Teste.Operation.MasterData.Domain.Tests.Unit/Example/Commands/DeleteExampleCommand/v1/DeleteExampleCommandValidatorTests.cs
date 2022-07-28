using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;
using Xunit;

namespace Bmb.Teste.Operation.MasterData.Domain.Tests.Unit.Example.Commands.DeleteExampleCommand.v1;

public class DeleteExampleCommandValidatorTests
{
    private readonly DeleteExampleCommandValidator _subject;

    public DeleteExampleCommandValidatorTests()
    {
        _subject = new DeleteExampleCommandValidator();
    }
    
    [Fact(DisplayName = "DeleteExampleCommandValidator throw invalid command")]
    public void Should_indicate_invalid_command()
    {
        var invalidCommand = new Domain.Example.Commands.DeleteExampleCommand.v1.DeleteExampleCommand(0);
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    [Fact(DisplayName = "DeleteExampleCommandValidator validate command successfully")]
    public void Should_validate_command_successfully()
    {
        var validCommand = new Domain.Example.Commands.DeleteExampleCommand.v1.DeleteExampleCommand(10);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}