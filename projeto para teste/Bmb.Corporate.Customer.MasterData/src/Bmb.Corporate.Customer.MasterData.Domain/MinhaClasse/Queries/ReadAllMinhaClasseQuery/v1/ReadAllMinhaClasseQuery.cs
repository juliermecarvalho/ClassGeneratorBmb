using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;

public class ReadAllMinhaClasseQuery : Query<IList<ReadAllMinhaClasseQueryResult>>
{
    public bool OnlyActive {get; }


    public ReadAllMinhaClasseQuery( bool onlyActive)
    {
        OnlyActive = onlyActive;
        
    }
}
