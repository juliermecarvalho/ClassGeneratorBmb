using Bmb.Core.Domain.Models;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;

public class DeleteExampleCommand : Command<EmptyResult?>
{
    public int Id { get; }

    public DeleteExampleCommand(int id)
    {
        Id = id;
    }
}