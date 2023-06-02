using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;

public class UpdateSegmentCommand : Command<UpdateSegmentCommandResult>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string Abbreviations { get; set; }
    public string UserId { get; set; }
}