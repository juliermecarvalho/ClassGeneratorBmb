using Microsoft.EntityFrameworkCore;

namespace WebGed.Core.Dominio.IRepositorio.Base
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        DbContext Context { get; }

        Task Commit();
    }
}
