using Bmb.Corporate.Customer.MasterData.Domain.Segment.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;
using Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Repositories.v1;

public class SegmentRepository : BaseRepository<Segment>, ISegmentRepository
{
    public SegmentRepository(BmbContext context) : base(context) { }
    
    public async Task<IList<Segment>> ReadAll(ReadAllSegmentQuery query, 
        CancellationToken cancellationToken = default)
    {
        try
        {

        var queryable = DbSet.AsQueryable();

        if (query.OnlyActive)
            queryable = queryable.Where(x => x.IsActive);

        
        return await queryable.ToListAsync(cancellationToken);
        }catch(Exception ex)
        {
            throw new Exception();
        }


    }
}