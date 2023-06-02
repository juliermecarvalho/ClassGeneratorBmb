using FluentValidation;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;

public class DeleteExampleCommandValidator : AbstractValidator<DeleteExampleCommand>
{
    public DeleteExampleCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}