using System.Linq.Expressions;
using WebGed.Core.Dominio.Core.Base;

namespace WebGed.Core.Dominio.IRepositorio.Base
{
    public interface IRepositorio<TEntidade> : IDisposable where TEntidade : Entidade
    {

        Task SalvarAsync(TEntidade entidade);
        Task ExluirAsync(int id);
        IQueryable<TEntidade> Listar();
        public Task CommitAsync();
        Task<TEntidade?> ObterAsync(int id);
        Task<TEntidade?> ObterAsync(int id, params Expression<Func<TEntidade, object>>[] includes);
        Task<TEntidade> ObterAsync(
           Expression<Func<TEntidade, bool>>? filter = null,
           params Expression<Func<TEntidade, object>>[] includes);
        Task<IEnumerable<TEntidade>> ListarAsync(
            Expression<Func<TEntidade, bool>>? filtro = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? ordenacao = null,
            params Expression<Func<TEntidade,
            object>>[] includes);
        Task<IEnumerable<TEntidade>> ListarAsync(
            Expression<Func<TEntidade, bool>>? filtro = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? ordenacao = null);
        Task<Paginacao<TEntidade>> ListarAsync(
            int pagina,
            Expression<Func<TEntidade, bool>>? filter = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
            params Expression<Func<TEntidade, object>>[] includes);
        Task<IEnumerable<TEntidade>> ListarAsync(
         Expression<Func<TEntidade, bool>>? filter = null,
         Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
         Expression<Func<TEntidade, int, TEntidade>>? select = null,
         params Expression<Func<TEntidade, object>>[] includes);
        Task<IEnumerable<TEntidade>> ListarAsync(
           Expression<Func<TEntidade, bool>>? filter = null,
           params Expression<Func<TEntidade, object>>[] includes);

    }


}
