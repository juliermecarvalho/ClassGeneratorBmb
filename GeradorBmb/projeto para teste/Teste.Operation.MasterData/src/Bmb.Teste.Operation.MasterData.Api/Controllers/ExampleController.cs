using Bmb.Core.Api.Controllers;
using Bmb.Core.Api.Responses;
using Bmb.Core.Domain.Contracts;
using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.DeleteExampleCommand.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Commands.UpdateExampleCommand.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadAllExampleQuery.v1;
using Bmb.Teste.Operation.MasterData.Domain.Example.Queries.ReadOneExampleQuery.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmptyResult = Bmb.Core.Domain.Models.EmptyResult;

namespace Bmb.Teste.Operation.MasterData.Api.Controllers;

[Route("v1/sample")]
public class ExampleController : RestControllerBase<ExampleController>
{
    public ExampleController(IMediator bus, INotificationContext notificationContext, 
        ILogger<ExampleController> logger) : base(bus, notificationContext, logger) { }
    
    [HttpPost]
    public async Task<ActionResult<Response<CreateExampleCommandResult>>> Post([FromBody] CreateExampleCommand request, 
        CancellationToken cancellationToken) => 
        await PostAsync<CreateExampleCommand, CreateExampleCommandResult>(request, cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<EmptyResult?>>> Delete([FromRoute] int id, CancellationToken cancellationToken) =>
        await DeleteAsync(new DeleteExampleCommand(id), cancellationToken);

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<UpdateExampleCommandResult>>> Put([FromRoute] int id,
        [FromBody] UpdateExampleCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        
        return await PutAsync<UpdateExampleCommand, UpdateExampleCommandResult>(request, cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ReadOneExampleQueryResult?>>> GetOne([FromRoute] int id,
        CancellationToken cancellationToken) =>
        await GetAsync<ReadOneExampleQuery, ReadOneExampleQueryResult?>(new ReadOneExampleQuery(id), cancellationToken);

    [HttpGet]
    public async Task<ActionResult<Response<IList<ReadAllExampleQueryResult>>>> GetAll([FromQuery] string? propertyOneFilter,
        bool? propertyTwo, CancellationToken cancellationToken, [FromQuery] bool onlyActive = true) =>
        await GetAsync<ReadAllExampleQuery, IList<ReadAllExampleQueryResult>>(
            new ReadAllExampleQuery(propertyOneFilter, propertyTwo, onlyActive), cancellationToken);
}