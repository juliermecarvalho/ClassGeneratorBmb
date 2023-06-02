using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;

public class ReadOneMinhaClasseQuery : Query<ReadOneMinhaClasseQueryResult>
{
    public int Id {get; }


    public ReadOneMinhaClasseQuery(int id)
    {
        Id = id;
        
    }
}
