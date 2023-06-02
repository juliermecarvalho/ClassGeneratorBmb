namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarQueriesAll
    {
        private DirectoryInfo _directory;
        private readonly IDictionary<string, string> _propertys;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        private string _nameUsing;

        public GerarQueriesAll(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
            _nameUsing = nameUsing;
        }


        public void Gerar()
        {
            DirectoryInfo directoryGetAllCommand = new(@$"{_directory.FullName}\{_nameClass}\Queries\GetAll\v1");
            if (!directoryGetAllCommand.Exists)
            {
                directoryGetAllCommand.Create();
            }

            GetAllCommandProfile(directoryGetAllCommand);
            GetAllQuery(directoryGetAllCommand);

            GetAllCommandHandler(directoryGetAllCommand);
            GetAllCommandResult(directoryGetAllCommand);



        }


        private void GetAllQuery(DirectoryInfo directoryGetAllCommand)
        {
            StreamWriter file = new(@$"{directoryGetAllCommand.FullName}\GetAll{_nameClass}Query.cs");
            string linhas = @$"
using BMB.Core.Domain.Models;

namespace {gerarNamespace(directoryGetAllCommand)};

public class GetAll{_nameClass}Query : Query<IList<GetAll{_nameClass}QueryResult>>
{abre}
    //public bool IsActive {abre}get; {fecha}

    //public GetAll{_nameClass}Query() {{ IsActive = true; }}

    //public GetAll{_nameClass}Query(bool isActive)
    //{abre}
    //    IsActive = isActive;
        
    //{fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void GetAllCommandHandler(DirectoryInfo directoryGetAllCommand)
        {
            StreamWriter file = new(@$"{directoryGetAllCommand.FullName}\GetAll{_nameClass}QueryHandler.cs");


            string linhas = @$"
using AutoMapper;
using BMB.Core.Domain.Contracts;
using BMB.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryGetAllCommand)};

public class GetAll{_nameClass}QueryHandler : Handler<GetAll{_nameClass}Query, IList<GetAll{_nameClass}QueryResult>>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public GetAll{_nameClass}QueryHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<IList<GetAll{_nameClass}QueryResult>> Handle(GetAll{_nameClass}Query request, 
        CancellationToken cancellationToken)
    {abre}
        return _mapper.Map<IList<GetAll{_nameClass}QueryResult>>(await _{_nameClass.ToLower()}Repository.GetAll(request, cancellationToken));        

    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void GetAllCommandProfile(DirectoryInfo directoryGetAllCommand)
        {
            StreamWriter file = new(@$"{directoryGetAllCommand.FullName}\GetAll{_nameClass}QueryProfile.cs");
            string linhas = @$"
using AutoMapper;
using BMB.Core.Domain.Contracts;
using BMB.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryGetAllCommand)};

public class GetAll{_nameClass}CommandProfile : Profile
{abre}
    public GetAll{_nameClass}CommandProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, GetAll{_nameClass}QueryResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void GetAllCommandResult(DirectoryInfo directoryGetAllCommand)
        {
            StreamWriter file = new(@$"{directoryGetAllCommand.FullName}\GetAll{_nameClass}QueryResult.cs");
            Assistant assistant = new();
            var p = assistant.GerarPropertys(_propertys);
            string linhas = @$"

namespace {gerarNamespace(directoryGetAllCommand)};

public class GetAll{_nameClass}QueryResult
{abre}
    public int Id {{ get; set; }}    
{p}
    //public bool IsActive {{ get; set; }}
    //public string CreatedBy {{ get; set; }}
    //public DateTime CreatedOn {{ get; set; }}
    //public string? ModifiedBy {{ get; set; }}
    //public DateTime? ModifiedOn {{ get; set; }}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private string gerarNamespace(DirectoryInfo directoryGetAllCommand)
        {
            var split = directoryGetAllCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
