using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQueryValidator : AbstractValidator<ReadOneSegmentQuery>
{
    public ReadOneSegmentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}