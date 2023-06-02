using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;

public class UpdateSegmentCommandValidator : AbstractValidator<UpdateSegmentCommand>
{
    public UpdateSegmentCommandValidator()
    {
        RuleFor(seg => seg.Name)
              .NotNull()
              .NotEmpty().WithMessage("The name must be not empty.")
              .MaximumLength(50).WithMessage("The name must be less than 50 characters.");



        RuleFor(seg => seg.IsActive)
            .NotNull();

        RuleFor(seg => seg.Abbreviations)
            .NotNull()
            .NotEmpty().WithMessage("The Abbreviations must be not empty.")
            .MinimumLength(3).WithMessage("The Abbreviations must have more than 8 characters.")
            .Length(3).WithMessage("The limit of 3 characteres was not respected.");
    }
}