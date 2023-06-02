using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1;

public class DeleteSegmentCommandValidator : AbstractValidator<DeleteSegmentCommand>
{
    public DeleteSegmentCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}