namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarDelete
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        const string notFound = "\"Not found\"";


        public GerarDelete(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }


        public void Gerar()
        {
            DirectoryInfo directoryDeleteCommand = new(@$"{_directory.FullName}\{_nameClass}\Commands\Delete{_nameClass}Command\v1");
            if (!directoryDeleteCommand.Exists)
            {
                directoryDeleteCommand.Create();
            }

            DeleteCommand(directoryDeleteCommand);
            DeleteCommandHandler(directoryDeleteCommand);

            DeleteCommandValidador(directoryDeleteCommand);



        }


        private void DeleteCommand(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\Delete{_nameClass}Command.cs");
            string linhas = @$"
using Bmb.Core.Domain.Models;

namespace {gerarNamespace(directoryDeleteCommand)};

public class Delete{_nameClass}Command :  Command<EmptyResult?>
{abre}
    public int Id {abre}get; {fecha}

    public Delete{_nameClass}Command(int id)
    {abre}
        Id = id;
    {fecha}

{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void DeleteCommandHandler(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\Delete{_nameClass}CommandHandler.cs");


            string linhas = @$"
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Bmb.Core.Domain.Handlers;
using Bmb.Core.Domain.Models;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;

namespace {gerarNamespace(directoryDeleteCommand)};

public class Delete{_nameClass}CommandHandler : Handler<Delete{_nameClass}Command, EmptyResult?>
{abre}

    private readonly I{_nameClass}Repository _{_nameClass.ToLower()}Repository;
    
    public Delete{_nameClass}CommandHandler(INotificationContext notificationContext, I{_nameClass}Repository {_nameClass}Repository) : 
        base(notificationContext)
    {abre}

            _{_nameClass.ToLower()}Repository = {_nameClass}Repository;
    {fecha}

    public override async Task<EmptyResult?> Handle(Delete{_nameClass}Command request, CancellationToken cancellationToken)
    {abre}
        var entity = await _{_nameClass.ToLower()}Repository.GetByIdAsync(request.Id, cancellationToken);
      
        if (entity == null)
        {abre}
            NotificationContext.Push(null, {notFound}, NotificationType.NotFound, request.GetCorrelation());
            return null;
        {fecha}


        await _{_nameClass.ToLower()}Repository.RemoveAsync(entity, cancellationToken);

        return new EmptyResult();


    {fecha}
{fecha}";

            file.WriteLine(linhas.Trim());
            file.Close();
        }


        private void DeleteCommandValidador(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\Delete{_nameClass}CommandValidator.cs");
            string linhas = @$"

using FluentValidation;

namespace Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Commands.Delete{_nameClass}Command.v1;

public class Delete{_nameClass}CommandValidator : AbstractValidator<Delete{_nameClass}Command>
{abre}
    public Delete{_nameClass}CommandValidator()
    {abre}
        RuleFor(x => x.Id).NotNull().NotEmpty();
    {fecha}
{fecha}
";
            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private string gerarNamespace(DirectoryInfo directoryDeleteCommand)
        {
            var split = directoryDeleteCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}
