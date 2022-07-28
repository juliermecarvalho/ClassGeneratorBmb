using Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Entities.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;
using Bmb.Teste.Operation.MasterData.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Teste.Operation.MasterData.Infra.Data.Repositories.v1;

public class ExampleRepository : BaseRepository<Example>, IExampleRepository
{
    public ExampleRepository(BmbContext context) : base(context) { }
    
    public async Task<IList<Example>> ReadAll(ReadAllExampleQuery query, 
        CancellationToken cancellationToken = default)
    {
        var queryable = DbSet.AsQueryable();

        if (query.OnlyActive)
            queryable = queryable.Where(x => x.IsActive);

        if (query.PropertyOneFilter is not null)
            queryable = queryable.Where(x => x.PropertyOne.Contains(query.PropertyOneFilter));

        if (query.PropertyTwo is not null)
            queryable = queryable.Where(x => x.PropertyTwo == query.PropertyTwo);
        
        return await queryable.ToListAsync(cancellationToken);
        
    }
}