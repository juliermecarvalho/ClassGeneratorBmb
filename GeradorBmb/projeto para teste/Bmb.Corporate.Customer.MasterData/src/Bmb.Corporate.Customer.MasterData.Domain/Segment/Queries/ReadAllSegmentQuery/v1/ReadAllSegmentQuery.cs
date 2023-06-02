using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;

public class ReadAllSegmentQuery : Query<IList<ReadAllSegmentQueryResult>>
{
    public bool OnlyActive { get; }


    public ReadAllSegmentQuery( bool onlyActive)
    {
        OnlyActive = onlyActive;
        
    }
}