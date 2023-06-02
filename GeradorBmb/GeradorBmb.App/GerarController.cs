namespace GeradorBmb.App
{
    public class GerarController
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        private string _nameUsing;
        const string tab = "    ";
        public GerarController(DirectoryInfo directoryInfo, string nameClass, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _nameUsing = nameUsing;
        }

        public void Gerar()
        {
            DirectoryInfo directoryControlle = new(@$"{_directory.FullName}\Controllers\");
            if (!directoryControlle.Exists)
            {
                throw new Exception("Diretório Controllers não existe");
            }

            StreamWriter file = new(@$"{_directory.FullName}\Controllers\{_nameClass}Controller.cs");
            gerarUsing(file, _nameClass);
            gerarNamespace(file, _nameClass, $"{_directory.Name}.{directoryControlle.Name}");
            gerarCorpo(file, _nameClass);
            file.Close();
        }

        private void gerarUsing(StreamWriter file, string nameClass)
        {
            file.WriteLine($"using BMB.Core.Api.Controllers;");
            file.WriteLine($"using BMB.Core.Api.Responses;");
            file.WriteLine($"using BMB.Core.Domain.Contracts;");
            file.WriteLine($"using {_nameUsing}.Domain.{nameClass}.Commands.Create.v1;");
            file.WriteLine($"using {_nameUsing}.Domain.{nameClass}.Commands.Delete.v1;");
            file.WriteLine($"using {_nameUsing}.Domain.{nameClass}.Commands.Update.v1;");
            file.WriteLine($"using {_nameUsing}.Domain.{nameClass}.Queries.GetAll.v1;");
            file.WriteLine($"using {_nameUsing}.Domain.{nameClass}.Queries.GetOne.v1;");
            file.WriteLine($"using MediatR;");
            file.WriteLine($"using Microsoft.AspNetCore.Mvc;");
            file.WriteLine($"using EmptyResult = Bmb.Core.Domain.Models.EmptyResult;");

        }

        private void gerarNamespace(StreamWriter file, string nameClass, string nameSpace)
        {
            file.WriteLine($"");
            file.WriteLine($"namespace  {nameSpace};");
        }

        private void gerarCorpo(StreamWriter file, string nameClass)
        {
            file.WriteLine("");
            file.WriteLine($"[Route(\"v1/{nameClass.ToLower()}\")]");
            file.WriteLine($"public class {nameClass}Controller : RestControllerBase<{nameClass}Controller>");
            file.WriteLine("{");
            file.WriteLine($"{tab}public {nameClass}Controller(IMediator bus, INotificationContext notificationContext,");
            file.Write($"{tab}{tab}ILogger<{nameClass}Controller> logger) : base(bus, notificationContext, logger) ");
            file.WriteLine("{ }");
            file.WriteLine("");

            file.WriteLine($"{tab}[HttpPost]");
            file.WriteLine($"{tab}public async Task<ActionResult<Response<Create{nameClass}CommandResult>>> Post([FromBody] Create{nameClass}Command request,");
            file.WriteLine($"{tab}{tab}CancellationToken cancellationToken) =>");
            file.WriteLine($"{tab}{tab}await PostAsync<Create{nameClass}Command, Create{nameClass}CommandResult>(request, cancellationToken);");
            file.WriteLine("");

            string parameter = "\"{id:int}\"";
            file.WriteLine($"{tab}[HttpDelete({parameter})]");
            file.WriteLine($"{tab}public async Task<ActionResult<Response<EmptyResult?>>> Delete([FromRoute] int id, CancellationToken cancellationToken) =>");
            file.WriteLine($"{tab}{tab}await DeleteAsync(new Delete{nameClass}Command(id), cancellationToken);");
            file.WriteLine("");

            file.WriteLine($"{tab}[HttpPut({parameter})]");
            file.WriteLine($"{tab}public async Task<ActionResult<Response<Update{nameClass}CommandResult>>> Put([FromRoute] int id,");
            file.WriteLine($"{tab}[FromBody] Update{nameClass}Command request, CancellationToken cancellationToken)");
            file.Write($"{tab}");
            file.WriteLine("{");
            file.WriteLine($"{tab}{tab}request.Id = id;");
            file.WriteLine($"{tab}{tab}return await PutAsync<Update{nameClass}Command, Update{nameClass}CommandResult>(request, cancellationToken);");
            file.Write($"{tab}");
            file.WriteLine("}");
            file.WriteLine("");

            file.WriteLine($"{tab}[HttpGet({parameter})]");
            file.WriteLine($"{tab}public async Task<ActionResult<Response<GetOne{nameClass}QueryResult?>>> GetOne([FromRoute] int id,");
            file.WriteLine($"{tab}{tab}CancellationToken cancellationToken) =>");
            file.WriteLine($"{tab}{tab}await GetAsync<GetOne{nameClass}Query, GetOne{nameClass}QueryResult?>(new GetOne{nameClass}Query(id), cancellationToken);");
            file.WriteLine("");

            file.WriteLine($"{tab}[HttpGet]");

            file.WriteLine($"{tab}public async Task<ActionResult<Response<IList<GetAll{nameClass}QueryResult>>>> GetAll([FromQuery] GetAll{nameClass}Query request, CancellationToken cancellationToken) =>");
            file.WriteLine($"{tab}{tab}await GetAsync<GetAll{nameClass}Query, IList<GetAll{nameClass}QueryResult>>(request, cancellationToken);");
            file.WriteLine("");

            file.WriteLine("}");
        }
    }
}
