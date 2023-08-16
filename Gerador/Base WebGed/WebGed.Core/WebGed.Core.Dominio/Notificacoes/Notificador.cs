using FluentValidation.Results;
using WebGed.Core.Dominio.Notificacoes.Interfaces;

namespace WebGed.Core.Dominio.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes = new List<Notificacao>();

        public List<Notificacao> ListarNotificacoes()
        {
            return _notificacoes;
        }

        public void Adicionar(string mensagem)
        {
            _notificacoes.Add(new Notificacao(mensagem));

            throw new Exception();
        }

        public void Adicionar(string[] mensagem)
        {
            foreach (var msg in mensagem)
            {
                _notificacoes.Add(new Notificacao(msg));
            }
            throw new Exception();
        }

        public void Adicionar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificacoes.Add(new Notificacao(error.ErrorMessage));
            }

            throw new Exception();
        }

        public void Adicionar(Exception exception)
        {
            string mensagemDeErro;

            if (exception is Exception)
            {
                mensagemDeErro = exception.Message;

                _notificacoes.Add(new Notificacao(mensagemDeErro));
            }

            throw new Exception();
        }

        public void LimparNotificacoes()
        {
            _notificacoes.Clear();
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }


    }
}
