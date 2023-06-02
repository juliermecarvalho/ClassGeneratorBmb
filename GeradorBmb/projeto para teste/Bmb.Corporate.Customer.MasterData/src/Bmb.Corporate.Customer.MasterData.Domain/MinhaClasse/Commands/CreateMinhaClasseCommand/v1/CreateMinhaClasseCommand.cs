using Bmb.Core.Domain.Models;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommand : Command<CreateMinhaClasseCommandResult>
{
    public string Name {get; set; }
    public bool IsActive {get; set; }
    public int Idade { get; set; } 
    public string Nome { get; set; } 

}
