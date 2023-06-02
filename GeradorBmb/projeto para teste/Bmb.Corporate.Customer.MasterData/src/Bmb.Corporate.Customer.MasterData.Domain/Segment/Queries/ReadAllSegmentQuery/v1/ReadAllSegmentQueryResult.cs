namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;

public class ReadAllSegmentQueryResult
{
    public int Id { get;  set; }
    public string Name { get;  set; }
    public bool IsActive { get;  set; }
    public string Abbreviations { get;  set; }
    public string UserId { get;  set; }
    public DateTime Date { get;  set; }
}