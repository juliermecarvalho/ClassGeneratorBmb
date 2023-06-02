using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseReadOneValidator : AbstractValidator<ReadOneMinhaClasseQuery>
{
    public ReadOneMinhaClasseReadOneValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

    }
}
