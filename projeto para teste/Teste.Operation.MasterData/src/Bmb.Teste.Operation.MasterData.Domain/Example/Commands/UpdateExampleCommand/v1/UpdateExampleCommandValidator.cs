using FluentValidation;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.UpdateExampleCommand.v1;

public class UpdateExampleCommandValidator : AbstractValidator<UpdateExampleCommand>
{
    public UpdateExampleCommandValidator()
    {
        RuleFor(x=>x.PropertyOne).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PropertyTwo).NotNull();
    }
}