using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommandValidator : AbstractValidator<CreateMinhaClasseCommand>
{
    public CreateMinhaClasseCommandValidator()
    {
        //RuleFor(seg => seg.Name)
        //.NotNull()
        //.NotEmpty().WithMessage(The name must be not empty)
        //.MaximumLength(50).WithMessage(The name must be less than 50 characters.);
    }
}
