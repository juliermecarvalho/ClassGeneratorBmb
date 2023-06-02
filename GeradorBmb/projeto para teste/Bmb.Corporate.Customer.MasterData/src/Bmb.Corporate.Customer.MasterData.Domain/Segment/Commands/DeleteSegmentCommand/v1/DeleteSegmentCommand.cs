using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1;

public class DeleteSegmentCommand : Command<EmptyResult?>
{
    public int Id { get; }

    public DeleteSegmentCommand(int id)
    {
        Id = id;
    }
}