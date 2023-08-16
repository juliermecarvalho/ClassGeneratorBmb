namespace WebGed.Core.Dominio.Core.Base
{
    public class Paginacao<TEntidade> where TEntidade : Entidade
    {
        public int TotalRegistros { get; set; }
        public int TotalPorPagina { get; set; }
        public int Pagina { get; set; }
        public List<TEntidade> Lista { get; set; } = new List<TEntidade>();
    }


}
