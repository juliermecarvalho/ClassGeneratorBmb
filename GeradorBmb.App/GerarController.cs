using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorBmb.App
{
    public class GerarController
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string tab = "    ";
        public GerarController(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
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


            file.WriteLine($"using Bmb.Core.Api.Controllers;");
            file.WriteLine($"using Bmb.Core.Api.Responses;");
            file.WriteLine($"using Bmb.Core.Domain.Contracts;");
            file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{nameClass}.Commands.Create{nameClass}Command.v1;");
            file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{nameClass}.Commands.Delete{nameClass}Command.v1;");
            file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{nameClass}.Commands.Update{nameClass}Command.v1;");
            file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{nameClass}.Queries.ReadAll{nameClass}Query.v1;");
            file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{nameClass}.Queries.ReadOne{nameClass}Query.v1;");
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
            file.WriteLine($"{tab}public async Task<ActionResult<Response<ReadOne{nameClass}QueryResult?>>> GetOne([FromRoute] int id,");
            file.WriteLine($"{tab}{tab}CancellationToken cancellationToken) =>");
            file.WriteLine($"{tab}{tab}await GetAsync<ReadOne{nameClass}Query, ReadOne{nameClass}QueryResult?>(new ReadOne{nameClass}Query(id), cancellationToken);");
            file.WriteLine("");

            file.WriteLine($"{tab}[HttpGet]");
            file.WriteLine($"{tab}public async Task<ActionResult<Response<IList<ReadAll{nameClass}QueryResult>>>> GetAll(");
            file.WriteLine($"{tab}{tab}CancellationToken cancellationToken, [FromQuery] bool onlyActive = true) =>");
            file.WriteLine($"{tab}{tab}await GetAsync<ReadAll{nameClass}Query, IList<ReadAll{nameClass}QueryResult>>(");
            file.WriteLine($"{tab}{tab}{tab}new ReadAll{nameClass}Query(onlyActive), cancellationToken);");
            file.WriteLine("");


            file.WriteLine("}");


        }
    }
}
