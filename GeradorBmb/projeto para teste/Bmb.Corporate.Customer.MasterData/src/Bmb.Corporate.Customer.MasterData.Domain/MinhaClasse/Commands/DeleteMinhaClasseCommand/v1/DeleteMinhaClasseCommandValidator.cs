using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;

public class DeleteMinhaClasseCommandValidator : AbstractValidator<DeleteMinhaClasseCommand>
{
    public DeleteMinhaClasseCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}
