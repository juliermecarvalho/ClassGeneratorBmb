using AutoMapper;
using WebGed.Core.Api.Models;
using WebGed.Core.Api.Models.Base;
using WebGed.Core.Dominio.Core;
using WebGed.Core.Dominio.Core.Base;

namespace WebGed.Core.Api.Configuration.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapperAplicativo();
            MapperPaginacao();
        }



        private void MapperAplicativo()
        {
            CreateMap<Aplicativo, AplicativoModel>().ReverseMap();
        }

        private void MapperPaginacao()
        {
            CreateMap<Paginacao<Entidade>, PaginacaoModel<ModelBase>>()
                .ForMember(d => d.TotalPorPagina, opt => opt.MapFrom(s => s.TotalPorPagina))
                .ForMember(d => d.TotalRegistros, opt => opt.MapFrom(s => s.TotalRegistros))
                .ForMember(d => d.Pagina, opt => opt.MapFrom(s => s.Pagina))
                .ForMember(d => d.Lista, opt => opt.Ignore());

            CreateMap<PaginacaoModel<ModelBase>, Paginacao<Entidade>>()
               .ForMember(d => d.TotalPorPagina, opt => opt.MapFrom(s => s.TotalPorPagina))
               .ForMember(d => d.TotalRegistros, opt => opt.MapFrom(s => s.TotalRegistros))
               .ForMember(d => d.Pagina, opt => opt.MapFrom(s => s.Pagina))
               .ForMember(d => d.Lista, opt => opt.Ignore());
        }
    }
}
