using Bmb.Core.Domain.Models;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommand : Command<CreateExampleCommandResult>
{
    public string? PropertyOne { get; set; }
    public bool PropertyTwo { get; set; }
}