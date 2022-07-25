using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommandValidator : AbstractValidator<UpdateMinhaClasseCommand>
{
    public UpdateMinhaClasseCommandValidator()
    {
        //RuleFor(seg => seg.Name)
        //.NotNull()
        //.NotEmpty().WithMessage(The name must be not empty)
        //.MaximumLength(50).WithMessage(The name must be less than 50 characters.);
    }
}
