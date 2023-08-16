using WebGed.Core.Api.Models;
using WebGed.Core.Api.Models.Base;
using WebGed.Core.Dominio.Core.Base;

namespace WebGed.Core.Api.Configuration.AutoMapper
{

    public static class ExtensionMapper
    {
        public static T Map<T>(this ModelBase obj) where T : Entidade
        {
            return AutoMapperConfig.Mapper.Map<T>(obj);
        }

        public static T Map<T>(this Entidade obj) where T : ModelBase
        {
            return AutoMapperConfig.Mapper.Map<T>(obj);
        }

        public static T Map<T>(this IList<Entidade> obj) where T : IList<ModelBase>
        {
            return AutoMapperConfig.Mapper.Map<T>(obj);
        }

        public static T Map<T>(this IList<ModelBase> obj) where T : IList<Entidade>
        {
            return AutoMapperConfig.Mapper.Map<T>(obj);
        }

        public static T Map<T, TEntidade>(this List<TEntidade> obj) where T : List<ModelBase> where TEntidade : Entidade
        {
            return AutoMapperConfig.Mapper.Map<T>(obj);
        }


        public static PaginacaoModel<T> Map<T, TEntidade>(this Paginacao<TEntidade> obj) where T : ModelBase where TEntidade : Entidade
        {
            var lista = obj.Lista.Select(x => x.Map<T>()).ToList();
            return new PaginacaoModel<T>
            {
                Lista = obj.Lista.Select(x => x.Map<T>()).ToList(),
                Pagina = obj.Pagina,
                TotalPorPagina = obj.TotalPorPagina,
                TotalRegistros = obj.TotalRegistros
            };
        }

    }

}
