using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Reflection;

namespace WebGed.Core.Api.Configuration.AutoMapper
{
    public class AutoMapperConfig
    {
        public static readonly IMapper Mapper = Configure();

        public static IMapper Configure()
        {
            var myAssembly = Assembly.GetExecutingAssembly();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(myAssembly);
                cfg.AddCollectionMappers();
            });

            return config.CreateMapper();
        }
    }
}