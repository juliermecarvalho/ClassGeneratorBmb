using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommand : Command<UpdateMinhaClasseCommandResult>
{
    public int Id {get; set; }
    public bool IsActive {get; set; }
    public int Idade { get; set; } 
    public string Nome { get; set; } 

}
