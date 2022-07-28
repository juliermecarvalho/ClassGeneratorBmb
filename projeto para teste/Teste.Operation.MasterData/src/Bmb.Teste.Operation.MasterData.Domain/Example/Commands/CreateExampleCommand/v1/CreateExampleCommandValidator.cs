using FluentValidation;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommandValidator : AbstractValidator<CreateExampleCommand>
{
    public CreateExampleCommandValidator()
    {
        RuleFor(x => x.PropertyOne).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PropertyTwo).NotNull();
    }
}