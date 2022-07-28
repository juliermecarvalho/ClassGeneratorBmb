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
            DirectoryInfo directoryReadOneCommand = new(@$"{_directory.FullName}\{_nameClass}\Queries\ReadOne{_nameClass}Query\v1");
            if (!directoryReadOneCommand.Exists)
            {
                directoryReadOneCommand.Create();
            }

            ReadOneQuery(directoryReadOneCommand);
            ReadOneCommandHandler(directoryReadOneCommand);
            ReadOneCommandResult(directoryReadOneCommand);

            ReadOneCommandProfile(directoryReadOneCommand);
            ReadOneCommandValidador(directoryReadOneCommand);




        }


        private void ReadOneQuery(DirectoryInfo directoryReadOneCommand)
        {
            StreamWriter file = new(@$"{directoryReadOneCommand.FullName}\ReadOne{_nameClass}Query.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryReadOneCommand)};

public class ReadOne{_nameClass}Query : Query<ReadOne{_nameClass}QueryResult>
{abre}
    public int Id {abre}get; {fecha}


    public ReadOne{_nameClass}Query(int id)
    {abre}
        Id = id;
        
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void ReadOneCommandHandler(DirectoryInfo directoryReadOneCommand)
        {
            StreamWriter file = new(@$"{directoryReadOneCommand.FullName}\ReadOne{_nameClass}QueryHandler.cs");


            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryReadOneCommand)};

public class ReadOne{_nameClass}QueryHandler : Handler<ReadOne{_nameClass}Query, ReadOne{_nameClass}QueryResult?>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public ReadOne{_nameClass}QueryHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<ReadOne{_nameClass}QueryResult?> Handle(ReadOne{_nameClass}Query request, 
        CancellationToken cancellationToken)
    {abre}
        var result = await _{_nameClass.ToLower()}Repository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) 
            return _mapper.Map<ReadOne{_nameClass}QueryResult>(result);
        
        NotificationContext.Push({notFound}, NotificationType.NotFound, request.GetCorrelation());
        return null;

    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void ReadOneCommandProfile(DirectoryInfo directoryReadOneCommand)
        {
            StreamWriter file = new(@$"{directoryReadOneCommand.FullName}\ReadOne{_nameClass}QueryProfile.cs");
            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryReadOneCommand)};

public class ReadOne{_nameClass}QueryProfile : Profile
{abre}
    public ReadOne{_nameClass}QueryProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, ReadOne{_nameClass}QueryResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void ReadOneCommandResult(DirectoryInfo directoryReadOneCommand)
        {
            StreamWriter file = new(@$"{directoryReadOneCommand.FullName}\ReadOne{_nameClass}QueryResult.cs");
            Assistant assistant = new();
            var p = assistant.GerarPropertys(_propertys);
            string linhas = @$"

namespace {gerarNamespace(directoryReadOneCommand)};

public class ReadOne{_nameClass}QueryResult
{abre}
    public int Id {abre} get; set; {fecha}
    public bool IsActive {abre} get; set; {fecha}
{p}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void ReadOneCommandValidador(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\ReadOne{_nameClass}QueryValidator.cs");
            string linhas = @$"

using FluentValidation;

namespace {gerarNamespace(directoryCreateCommand)};

public class ReadOne{_nameClass}QueryValidator : AbstractValidator<ReadOne{_nameClass}Query>
{abre}
    public ReadOne{_nameClass}QueryValidator()
    {abre}
        RuleFor(x => x.Id).NotEmpty();

    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private string gerarNamespace(DirectoryInfo directoryReadOneCommand)
        {
            var split = directoryReadOneCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
