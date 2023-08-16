using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebGed.Core.Dominio.Extensions;
using WebGed.Core.Dominio.IRepositorio.Base;
using WebGed.Core.Dominio.Core.Base;
using WebGed.Core.Dominio.Notificacoes.Interfaces;

namespace WebGed.Persistencia.Repositorio.Base
{
    public abstract class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
    {
        protected readonly DbContext Context;
        protected readonly IUnidadeDeTrabalho UnidadeDeTrabalho;
        private readonly int _totalDeRegistrosPorPagina = 10;
        private readonly INotificador _notificador;


        protected Repositorio(IUnidadeDeTrabalho unidadeDeTrabalho, INotificador notificador)
        {
            UnidadeDeTrabalho = unidadeDeTrabalho;
            Context = unidadeDeTrabalho.Context;
            _notificador = notificador;
        }

        public async Task SalvarAsync(TEntidade entidade)
        {
            if (entidade.Id == 0)
            {
                await Context.AddAsync(entidade);
            }
            else
            {
                Context.Update(entidade);
            }

        }
        public async Task ExluirAsync(int id)
        {
            var obj = await ObterAsync(id);
            if(obj != null && obj.Id > 0)
            {
                _ = Context.Remove(obj);
            }

            _notificador.Adicionar("Erro ao excluir registro. Registro não pode ser encontrado");
        }
        public async Task<TEntidade?> ObterAsync(int id)
        {
            return await Context.Set<TEntidade>().FindAsync(id);
        }
        public async Task<TEntidade?> ObterAsync(int id, params Expression<Func<TEntidade, object>>[] includes)
        {
            var result = includes.Aggregate(
                 Context.Set<TEntidade>().AsQueryable(),
                 (current, include) => current.Include(include.AsPath()));

            return await result.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task<IEnumerable<TEntidade>> ListarAsync(
            Expression<Func<TEntidade, bool>>? filter = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,

            params Expression<Func<TEntidade, object>>[] includes)
        {
            var query = Context.Set<TEntidade>().AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            query = includes
             .Aggregate(
                 query,
                 (current, include) => current.Include(include.AsPath())
             );

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync(
           Expression<Func<TEntidade, bool>>? filter = null,
           params Expression<Func<TEntidade, object>>[] includes)
        {
            var query = Context.Set<TEntidade>().AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            query = includes
             .Aggregate(
                 query,
                 (current, include) => current.Include(include.AsPath())
             );

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync(
         Expression<Func<TEntidade, bool>>? filter = null,
         Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
         Expression<Func<TEntidade, int, TEntidade>>? select = null,
         params Expression<Func<TEntidade, object>>[] includes)
        {
            var query = Context.Set<TEntidade>().AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            query = includes
             .Aggregate(
                 query,
                 (current, include) => current.Include(include.AsPath())
             );

            if (select is not null)
            {
                query = query.Select(select);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntidade>> ListarAsync(Expression<Func<TEntidade, bool>>? filter = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null)
        {
            var query = Context.Set<TEntidade>().AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntidade> ObterAsync(
         Expression<Func<TEntidade, bool>>? filter = null,
         params Expression<Func<TEntidade, object>>[] includes)
        {
            var query = Context.Set<TEntidade>().AsQueryable();
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            query = includes
             .Aggregate(
                 query,
                 (current, include) => current.Include(include.AsPath())
             );

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Paginacao<TEntidade>> ListarAsync(
            int pagina,
            Expression<Func<TEntidade, bool>>? filter = null,
            Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
            params Expression<Func<TEntidade, object>>[] includes)
        {
            if(pagina < 1)
            {
                _notificador.Adicionar("página tem ser maior ou igual a 1");
            }

            var query = Context.Set<TEntidade>().AsQueryable();

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            query = includes
             .Aggregate(
                 query,
                 (current, include) => current.Include(include.AsPath())
             );



            var listaPaginada = await query
                //.OrderBy(e => e.Id)
                .Skip(_totalDeRegistrosPorPagina * (pagina - 1))
                .Take(_totalDeRegistrosPorPagina).ToListAsync();

            var retornoListaComPaginacao = new Paginacao<TEntidade>
            {
                TotalRegistros = query.Count(),
                TotalPorPagina = _totalDeRegistrosPorPagina,
                Pagina = pagina,
                Lista = listaPaginada
            };

            return retornoListaComPaginacao;
        }

        public async Task CommitAsync()
        {
            await UnidadeDeTrabalho.Commit();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public IQueryable<TEntidade> Listar()
        {
            return Context.Set<TEntidade>().AsQueryable();
        }
    }
}
