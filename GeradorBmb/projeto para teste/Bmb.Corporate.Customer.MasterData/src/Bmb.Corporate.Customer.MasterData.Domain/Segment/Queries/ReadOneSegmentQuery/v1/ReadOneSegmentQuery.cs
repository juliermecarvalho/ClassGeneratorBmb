using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQuery : Query<ReadOneSegmentQueryResult?>
{
    public int Id { get; }

    public ReadOneSegmentQuery(int id)
    {
        Id = id;
    }
}