using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;

public class DeleteMinhaClasseCommand :  Command<EmptyResult?>
{
    public int Id {get; }

    public DeleteMinhaClasseCommand(int id)
    {
        Id = id;
    }

}
