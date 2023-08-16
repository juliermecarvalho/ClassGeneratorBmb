using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebGed.Core.Api.Configuration.Validation;
using WebGed.Core.Dominio.IRepositorio;
using WebGed.Core.Dominio.IRepositorio.Base;
using WebGed.Core.Dominio.Notificacoes;
using WebGed.Core.Dominio.Notificacoes.Interfaces;
using WebGed.Persistencia.Contexto;
using WebGed.Persistencia.Repositorio;
using WebGed.Persistencia.Repositorio.Base;

namespace WebGed.Core.Api.Configuration.DI
{
    public static class DIConfig
    {
        public static IServiceCollection ResolverDependencias(this IServiceCollection services)
        {


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<WGDbContext>(
            options => options.UseSqlServer("name=ConnectionStrings:webged"));
            services.AddSingleton<INotificador, Notificador>();

            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
            services.AddScoped<IRepositorioAplicativo, RepositorioAplicativo>();

            services.AddValidatorsFromAssemblyContaining(typeof(AplicativoModelValidator));
            return services;
        }

    }
}
