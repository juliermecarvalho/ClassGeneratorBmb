using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandValidator : AbstractValidator<CreateSegmentCommand>
{
    public CreateSegmentCommandValidator()
    {
        RuleFor(seg => seg.Name)
              .NotNull()
              .NotEmpty().WithMessage("The name must be not empty.")
              .MaximumLength(50).WithMessage("The name must be less than 50 characters.");

        RuleFor(seg => seg.UserId)
            .NotNull()
            .NotEmpty().WithMessage("The userId must be less than 8 characters")
            .Length(8).WithMessage("The name must have 8 characters.");

        RuleFor(seg => seg.IsActive)
            .NotNull();

        RuleFor(seg => seg.Abbreviations)
            .NotNull()
            .NotEmpty().WithMessage("The Abbreviations must be not empty.")
            .MinimumLength(3).WithMessage("The Abbreviations must have more than 8 characters.")
            .Length(3).WithMessage("The limit of 3 characteres was not respected.");
    }
}