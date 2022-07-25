namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarUpdate
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";

        public GerarUpdate(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }


        public void Gerar()
        {
            DirectoryInfo directoryUpdateCommand = new(@$"{_directory.FullName}\{_nameClass}\Commands\Update{_nameClass}Command\v1");
            if (!directoryUpdateCommand.Exists)
            {
                directoryUpdateCommand.Create();
            }

            UpdateCommand(directoryUpdateCommand);
            UpdateCommandHandler(directoryUpdateCommand);
            UpdateCommandProfile(directoryUpdateCommand);
            UpdateCommandResult(directoryUpdateCommand);
            UpdateCommandValidador(directoryUpdateCommand);



        }


        private void UpdateCommand(DirectoryInfo directoryUpdateCommand)
        {
            StreamWriter file = new(@$"{directoryUpdateCommand.FullName}\Update{_nameClass}Command.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryUpdateCommand)};

public class Update{_nameClass}Command : Command<Update{_nameClass}CommandResult>
{abre}
    public int Id {abre}get; set; {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void UpdateCommandHandler(DirectoryInfo directoryUpdateCommand)
        {
            StreamWriter file = new(@$"{directoryUpdateCommand.FullName}\Update{_nameClass}CommandHandler.cs");


            string linhas = @$"
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryUpdateCommand)};

public class Update{_nameClass}CommandHandler : Handler<Update{_nameClass}Command, Update{_nameClass}CommandResult>
{abre}
    private readonly IMapper _mapper;
    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public Update{_nameClass}CommandHandler(INotificationContext notificationContext, IMapper mapper, 
        I{_nameClass}Repository {_nameClass}Repository) : base(notificationContext)
    {abre}
            _mapper = mapper;
            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<Update{_nameClass}CommandResult?> Handle(Update{_nameClass}Command request, 
        CancellationToken cancellationToken)
    {abre}
        var entity = _mapper.Map<Entities.v1.{_nameClass}>(request);
      
        if (entity == null)
        {abre}
            NotificationContext.Push(null, string.Empty, NotificationType.NotFound,
                request.GetCorrelation());

            return null;
        {fecha}

        //entity.Change{_nameClass}(request.Name, request.Abbreviations, request.UserId, request.IsActive);

        await _{_nameClass.ToLower()}Repository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<Update{_nameClass}CommandResult>(entity);


    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void UpdateCommandProfile(DirectoryInfo directoryUpdateCommand)
        {
            StreamWriter file = new(@$"{directoryUpdateCommand.FullName}\Update{_nameClass}CommandProfile.cs");
            string linhas = @$"
using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Update{_nameClass}Command.v1;

public class Update{_nameClass}CommandProfile : Profile
{abre}
    public Update{_nameClass}CommandProfile()
    {abre}
        CreateMap<Entities.v1.{_nameClass}, Update{_nameClass}Command>().ReverseMap();
        CreateMap<Entities.v1.{_nameClass}, Update{_nameClass}CommandResult>().ReverseMap();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void UpdateCommandResult(DirectoryInfo directoryUpdateCommand)
        {
            StreamWriter file = new(@$"{directoryUpdateCommand.FullName}\Update{_nameClass}CommandResult.cs");
            string linhas = @$"

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Update{_nameClass}Command.v1;

public class Update{_nameClass}CommandResult
{abre}

{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void UpdateCommandValidador(DirectoryInfo directoryUpdateCommand)
        {
            StreamWriter file = new(@$"{directoryUpdateCommand.FullName}\Update{_nameClass}CommandValidator.cs");
            string linhas = @$"

using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Update{_nameClass}Command.v1;

public class Update{_nameClass}CommandValidator : AbstractValidator<Update{_nameClass}Command>
{abre}
    public Update{_nameClass}CommandValidator()
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

        private string gerarNamespace(DirectoryInfo directoryUpdateCommand)
        {
            var split = directoryUpdateCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
