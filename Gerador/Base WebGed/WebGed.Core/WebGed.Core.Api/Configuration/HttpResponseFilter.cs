using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebGed.Core.Dominio.Notificacoes.Interfaces;

namespace TiibSigner.Api.Configuration
{
    public class HttpResponseFilter : IActionFilter, IOrderedFilter
    {
        private readonly INotificador _notificador;

        public int Order { get; set; } = int.MaxValue - 10;
        private readonly ILogger<HttpResponseFilter> _logger;
        public HttpResponseFilter(ILogger<HttpResponseFilter> logger, INotificador notificador)
        {
            _logger = logger;
            _notificador = notificador;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _notificador.LimparNotificacoes();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notificador.TemNotificacao())
            {
                var mensagensNotificacao = _notificador.ListarNotificacoes().Select(n => n.Mensagem);

                context.Result = new ObjectResult(mensagensNotificacao)
                {
                    StatusCode = 417
                };
                _logger.LogWarning(string.Join($";{Environment.NewLine}", mensagensNotificacao.Select(x => x)));
                context.ExceptionHandled = true;
            }
            else
            {
                if (context?.Exception is Exception exception)
                {

                    var mensagemDeErro = $"Ocorreu um erro inesperado no servidor ao tentar processar a sua requisição.{Environment.NewLine}" +
                                         $"Mensagem de Erro: {exception.Message} {exception.InnerException?.Message}.";

                    context.Result = new ObjectResult(mensagemDeErro)
                    {
                        StatusCode = 500,
                    };

                    _logger.LogError(exception, exception.Message);
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
