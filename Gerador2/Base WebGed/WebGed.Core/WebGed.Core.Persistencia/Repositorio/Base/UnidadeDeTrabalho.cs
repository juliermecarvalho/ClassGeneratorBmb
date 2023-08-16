using Microsoft.EntityFrameworkCore;
using WebGed.Core.Dominio.IRepositorio.Base;
using WebGed.Persistencia.Contexto;

namespace WebGed.Persistencia.Repositorio.Base
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private readonly DbContext _context;

        public UnidadeDeTrabalho(WGDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
