namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;

public class ReadOneSegmentQueryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string Abbreviations { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
}