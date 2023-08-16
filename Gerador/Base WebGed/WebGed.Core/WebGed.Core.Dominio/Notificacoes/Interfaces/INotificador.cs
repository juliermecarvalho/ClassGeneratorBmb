using FluentValidation.Results;

namespace WebGed.Core.Dominio.Notificacoes.Interfaces
{
    public interface INotificador
    {
        void Adicionar(Exception exception);
        void Adicionar(string mensagem);
        void Adicionar(string[] mensagem);
        void Adicionar(ValidationResult validationResult);
        void LimparNotificacoes();
        List<Notificacao> ListarNotificacoes();
        bool TemNotificacao();
    }
}