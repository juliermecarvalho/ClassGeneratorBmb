
using WebGed.Core.Api.Models.Base;

namespace WebGed.Core.Api.Models
{

    public class PaginacaoModel<TEntidade> where TEntidade : ModelBase
    {
        public int TotalRegistros { get; set; }
        public int TotalPorPagina { get; set; }
        public int Pagina { get; set; }
        public List<TEntidade> Lista { get; set; } = new List<TEntidade>();
    }
}
