using FluentValidation;
using WebGed.Core.Api.Models;

namespace WebGed.Core.Api.Configuration.Validation
{
    public class AplicativoModelValidator : AbstractValidator<AplicativoModel>
    {
        public AplicativoModelValidator()
        {

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}