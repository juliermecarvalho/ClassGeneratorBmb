using Bmb.Core.Domain.Entities;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Entities.v1;

public class MinhaClasse : Entity
{
    public int Idade { get; private set; } 
    public string Nome { get; private set; } 

   public MinhaClasse() {}


    public MinhaClasse(int id, bool isActive, int idade, string nome) 
    {
        Idade = idade;
        Nome = nome;

    }
                
    public MinhaClasse(int idade, string nome) 
    {
        Idade = idade;
        Nome = nome;

    }
                
    public void ChangeMinhaClasse(int idade, string nome) 
    {
        Idade = idade;
        Nome = nome;

    }


}
