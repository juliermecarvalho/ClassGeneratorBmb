using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Entities.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;
using Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Repositories.v1;

public class MinhaClasseRepository : BaseRepository<MinhaClasse>, IMinhaClasseRepository
{
     public MinhaClasseRepository(BmbContext context) : base(context) { }

     public async Task<IList<MinhaClasse>> ReadAll(ReadAllMinhaClasseQuery query,
         CancellationToken cancellationToken = default)
     {
         var queryable = DbSet.AsQueryable();

         if (query.OnlyActive)
             queryable = queryable.Where(x => x.IsActive);

         return await queryable.ToListAsync(cancellationToken);
     }
}
