using Bmb.Core.Domain.Contracts;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Contracts.Repositories.v1;

public interface IExampleRepository : IBaseRepository<Entities.v1.Example>
{
    Task RemoveAsync(Entities.v1.Example entity, CancellationToken cancellationToken = default);
    Task<IList<Entities.v1.Example>> ReadAll(ReadAllExampleQuery query, CancellationToken cancellationToken = default);
}