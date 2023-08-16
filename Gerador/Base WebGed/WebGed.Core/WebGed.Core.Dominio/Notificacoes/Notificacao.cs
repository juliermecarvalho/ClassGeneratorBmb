namespace WebGed.Core.Dominio.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }

        public virtual string Mensagem { get; }
    }
}
