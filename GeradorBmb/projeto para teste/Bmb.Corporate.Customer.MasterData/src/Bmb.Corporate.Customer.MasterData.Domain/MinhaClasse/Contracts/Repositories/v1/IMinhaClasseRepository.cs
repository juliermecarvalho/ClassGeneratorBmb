using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Contracts.Repositories.v1;

public interface IMinhaClasseRepository : Core.Domain.Contracts.IBaseRepository<Entities.v1.MinhaClasse>
{
    Task RemoveAsync(Entities.v1.MinhaClasse entity, CancellationToken cancellationToken = default);
    Task<IList<Entities.v1.MinhaClasse>> ReadAll(ReadAllMinhaClasseQuery query, CancellationToken cancellationToken = default);   

}
