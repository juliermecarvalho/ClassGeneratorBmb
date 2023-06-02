using Bmb.Core.Domain.Models;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;

public class ReadAllExampleQuery : Query<IList<ReadAllExampleQueryResult>>
{
    public bool OnlyActive { get; }
    public string? PropertyOneFilter { get; }
    public bool? PropertyTwo { get; }

    public ReadAllExampleQuery(string? propertyOneFilter, bool? propertyTwo, bool onlyActive)
    {
        OnlyActive = onlyActive;
        PropertyTwo = propertyTwo;
        PropertyOneFilter = propertyOneFilter;
    }
}