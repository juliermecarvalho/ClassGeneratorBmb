namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarQueriesAll
    {
        private DirectoryInfo _directory;
        private readonly IDictionary<string, string> _propertys;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";

        public GerarQueriesAll(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
        }


        public void Gerar()
        {
            DirectoryInfo directoryReadAllCommand = new(@$"{_directory.FullName}\{_nameClass}\Queries\ReadAll{_nameClass}Query\v1");
            if (!directoryReadAllCommand.Exists)
            {
                directoryReadAllCommand.Create();
            }

            ReadAllCommandProfile(directoryReadAllCommand);
            ReadAllQuery(directoryReadAllCommand);

            ReadAllCommandHandler(directoryReadAllCommand);
            ReadAllCommandResult(directoryReadAllCommand);



        }


        private void ReadAllQuery(DirectoryInfo directoryReadAllCommand)
        {
            StreamWriter file = new(@$"{directoryReadAllCommand.FullName}\ReadAll{_nameClass}Query.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryReadAllCommand)};

public class ReadAll{_nameClass}Query : Query<IList<ReadAll{_nameClass}QueryResult>>
{abre}
    public bool OnlyActive {abre}get; {fecha}


    public ReadAll{_nameClass}Query(bool onlyActive)
    {abre}
        OnlyActive = onlyActive;
        
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void ReadAllCommandHandler(DirectoryInfo directoryReadAllCommand)
        {
            StreamWriter file = new(@$"{directoryReadAllCommand.FullName}\ReadAll{_nameClass}QueryHandler.cs");


            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryReadAllCommand)};

public class ReadAll{_nameClass}QueryHandler : Handler<ReadAll{_nameClass}Query, IList<ReadAll{_nameClass}QueryResult>>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public ReadAll{_nameClass}QueryHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<IList<ReadAll{_nameClass}QueryResult>> Handle(ReadAll{_nameClass}Query request, 
        CancellationToken cancellationToken)
    {abre}
        return _mapper.Map<IList<ReadAll{_nameClass}QueryResult>>(await _{_nameClass.ToLower()}Repository.ReadAll(request, cancellationToken));        

    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void ReadAllCommandProfile(DirectoryInfo directoryReadAllCommand)
        {
            StreamWriter file = new(@$"{directoryReadAllCommand.FullName}\ReadAll{_nameClass}QueryProfile.cs");
            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryReadAllCommand)};

public class ReadAll{_nameClass}CommandProfile : Profile
{abre}
    public ReadAll{_nameClass}CommandProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, ReadAll{_nameClass}QueryResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void ReadAllCommandResult(DirectoryInfo directoryReadAllCommand)
        {
            StreamWriter file = new(@$"{directoryReadAllCommand.FullName}\ReadAll{_nameClass}QueryResult.cs");
            Assistant assistant = new();
            var p = assistant.GerarPropertys(_propertys);
            string linhas = @$"

namespace {gerarNamespace(directoryReadAllCommand)};

public class ReadAll{_nameClass}QueryResult
{abre}
    public int Id {abre} get; set; {fecha}
    public bool IsActive {abre} get; set; {fecha}
{p}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private string gerarNamespace(DirectoryInfo directoryReadAllCommand)
        {
            var split = directoryReadAllCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
