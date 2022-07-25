namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarCreate
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";

        public GerarCreate(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }


        public void Gerar()
        {
            DirectoryInfo directoryCreateCommand = new(@$"{_directory.FullName}\{_nameClass}\Commands\Create{_nameClass}Command\v1");
            if (!directoryCreateCommand.Exists)
            {
                directoryCreateCommand.Create();
            }

            CreateCommand(directoryCreateCommand);
            CreateCommandHandler(directoryCreateCommand);
            CreateCommandProfile(directoryCreateCommand);
            CreateCommandResult(directoryCreateCommand);
            CreateCommandValidador(directoryCreateCommand);

        }


        private void CreateCommand(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\Create{_nameClass}Command.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryCreateCommand)};

public class Create{_nameClass}Command : Command<Create{_nameClass}CommandResult>
{abre}

{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void CreateCommandHandler(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\Create{_nameClass}CommandHandler.cs");


            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryCreateCommand)};

public class Create{_nameClass}CommandHandler : Handler<Create{_nameClass}Command, Create{_nameClass}CommandResult>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public Create{_nameClass}CommandHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<Create{_nameClass}CommandResult> Handle(Create{_nameClass}Command request, 
        CancellationToken cancellationToken)
    {abre}
        var entity = _mapper.Map<Entities.v1.{_nameClass}>(request);
        var id = await _{_nameClass.ToLower()}Repository.AddAsync(entity, cancellationToken);
    
        return return _mapper.Map<Create{_nameClass}CommandResult>(entity);
    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void CreateCommandProfile(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\Create{_nameClass}CommandProfile.cs");
            string linhas = @$"
using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Create{_nameClass}Command.v1;

public class Create{_nameClass}CommandProfile : Profile
{abre}
    public Create{_nameClass}CommandProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, Create{_nameClass}Command>().ReverseMap();
        CreateMap<Entities.v1.{_nameClass}, Create{_nameClass}CommandResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void CreateCommandResult(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\Create{_nameClass}CommandResult.cs");
            string linhas = @$"

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Create{_nameClass}Command.v1;

public class Create{_nameClass}CommandResult
{abre}

{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void CreateCommandValidador(DirectoryInfo directoryCreateCommand)
        {
            StreamWriter file = new(@$"{directoryCreateCommand.FullName}\Create{_nameClass}CommandValidator.cs");
            string linhas = @$"

using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Create{_nameClass}Command.v1;

public class Create{_nameClass}CommandValidator : AbstractValidator<Create{_nameClass}Command>
{abre}
    public Create{_nameClass}CommandValidator()
    {abre}
        //RuleFor(seg => seg.Name)
        //.NotNull()
        //.NotEmpty().WithMessage(The name must be not empty)
        //.MaximumLength(50).WithMessage(The name must be less than 50 characters.);
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private string gerarNamespace(DirectoryInfo directoryCreateCommand)
        {
            var split = directoryCreateCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
