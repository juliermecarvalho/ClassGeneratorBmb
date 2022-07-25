using Bmb.Core.Domain.Entities;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Entities.v1;

public class Segment : Entity
{
    public Segment() { }
    public Segment(string name, bool segmentSituation, string abbreviations)
    {
        Name = name;
        IsActive = segmentSituation;
        Abbreviations = abbreviations;

    }

    public Segment(int id, string name, bool isActive, string abbreviations, string userId)
    {

        Id = id;
        Name = name;
        IsActive = isActive;
        Abbreviations = abbreviations;
        UserId = userId;
        Date = DateTime.Now;
    }

    public string Name { get; private set; }
    public string Abbreviations { get; private set; }
    public string UserId { get; private set; }
    public DateTime Date { get; private set; }

    public void ChangeSituation(bool isActive, string userCode)
    {
        UserId = userCode;
        IsActive = isActive;
        Date = DateTime.Now;
    }

    public void ChangeSegment(string name, string abbreviations, string userId, bool situation)
    {
        Abbreviations = abbreviations;
        Name = name;
        UserId = userId;
        IsActive = situation;
        Date = DateTime.Now;
    }
    public void SetUser(string userId)
    {
        this.UserId = userId;
    }
}
