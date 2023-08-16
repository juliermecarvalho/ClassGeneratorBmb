using WebGed.Core.Dominio.Core;
using WebGed.Core.Dominio.IRepositorio;
using WebGed.Core.Dominio.IRepositorio.Base;
using WebGed.Core.Dominio.Notificacoes.Interfaces;
using WebGed.Persistencia.Repositorio.Base;

namespace WebGed.Persistencia.Repositorio
{
    public class RepositorioAplicativo : Repositorio<Aplicativo>, IRepositorioAplicativo
    {
        public RepositorioAplicativo(IUnidadeDeTrabalho unidadeDeTrabalho, INotificador notificador) : 
            base(unidadeDeTrabalho, notificador)
        {
        }
    }
}
