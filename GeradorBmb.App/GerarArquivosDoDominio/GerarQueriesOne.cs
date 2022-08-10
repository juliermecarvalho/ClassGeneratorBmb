namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarQueriesOne
    {
        private DirectoryInfo _directory;
        private readonly IDictionary<string, string> _propertys;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        const string notFound = "\"Not found\"";
        private string _nameUsing;

        public GerarQueriesOne(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
            _nameUsing = nameUsing;
        }


        public void Gerar()
        {
            DirectoryInfo directoryGetOneCommand = new(@$"{_directory.FullName}\{_nameClass}\Queries\GetOne\v1");
            if (!directoryGetOneCommand.Exists)
            {
                directoryGetOneCommand.Create();
            }

            GetOneQuery(directoryGetOneCommand);
            GetOneCommandHandler(directoryGetOneCommand);
            GetOneCommandResult(directoryGetOneCommand);

            GetOneCommandProfile(directoryGetOneCommand);
            GetOneCommandValidador(directoryGetOneCommand);




        }


        private void GetOneQuery(DirectoryInfo directoryGetOneCommand)
        {
            StreamWriter file = new(@$"{directoryGetOneCommand.FullName}\GetOne{_nameClass}Query.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryGetOneCommand)};

public class GetOne{_nameClass}Query : Query<GetOne{_nameClass}QueryResult>
{abre}
    public int Id {abre}get; {fecha}


    public GetOne{_nameClass}Query(int id)
    {abre}
        Id = id;
        
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void GetOneCommandHandler(DirectoryInfo directoryGetOneCommand)
        {
            StreamWriter file = new(@$"{directoryGetOneCommand.FullName}\GetOne{_nameClass}QueryHandler.cs");


            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryGetOneCommand)};

public class GetOne{_nameClass}QueryHandler : Handler<GetOne{_nameClass}Query, GetOne{_nameClass}QueryResult?>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public GetOne{_nameClass}QueryHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<GetOne{_nameClass}QueryResult?> Handle(GetOne{_nameClass}Query request, 
        CancellationToken cancellationToken)
    {abre}
        var result = await _{_nameClass.ToLower()}Repository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) 
            return _mapper.Map<GetOne{_nameClass}QueryResult>(result);
        
        NotificationContext.Push({notFound}, NotificationType.NotFound, request.GetCorrelation());
        return null;

    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void GetOneCommandProfile(DirectoryInfo directoryGetOneCommand)
        {
            StreamWriter file = new(@$"{directoryGetOneCommand.FullName}\GetOne{_nameClass}QueryProfile.cs");
            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryGetOneCommand)};

public class GetOne{_nameClass}QueryProfile : Profile
{abre}
    public GetOne{_nameClass}QueryProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, GetOne{_nameClass}QueryResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void GetOneCommandResult(DirectoryInfo directoryGetOneCommand)
        {
            StreamWriter file = new(@$"{directoryGetOneCommand.FullName}\GetOne{_nameClass}QueryResult.cs");
            Assistant assistant = new();
            var p = assistant.GerarPropertys(_propertys);
            string linhas = @$"

namespace {gerarNamespace(directoryGetOneCommand)};

public class GetOne{_nameClass}QueryResult
{abre}
    public int Id {abre} get; set; {fecha}
    public bool IsActive {abre} get; set; {fecha}
{p}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void GetOneCommandValidador(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\GetOne{_nameClass}QueryValidator.cs");
            string linhas = @$"

using FluentValidation;

namespace {gerarNamespace(directoryCreateCommand)};

public class GetOne{_nameClass}QueryValidator : AbstractValidator<GetOne{_nameClass}Query>
{abre}
    public GetOne{_nameClass}QueryValidator()
    {abre}
        RuleFor(x => x.Id).NotEmpty();

    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private string gerarNamespace(DirectoryInfo directoryGetOneCommand)
        {
            var split = directoryGetOneCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
