using Bmb.Core.Domain.Models;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;

public class ReadOneExampleQuery : Query<ReadOneExampleQueryResult?>
{
    public int Id { get; }

    public ReadOneExampleQuery(int id)
    {
        Id = id;
    }
}