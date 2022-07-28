using FluentValidation;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;

public class ReadOneExampleQueryValidator : AbstractValidator<ReadOneExampleQuery>
{
    public ReadOneExampleQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}