using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;

public interface ISegmentRepository : IBaseRepository<Entities.v1.Segment>
{
    Task RemoveAsync(Entities.v1.Segment entity, CancellationToken cancellationToken = default);
    Task<IList<Entities.v1.Segment>> ReadAll(ReadAllSegmentQuery query, CancellationToken cancellationToken = default);
}