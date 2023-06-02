using Bmb.Core.Api.Controllers;
using Bmb.Core.Api.Responses;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.DeleteMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadAllMinhaClasseQuery.v1;
using Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Queries.ReadOneMinhaClasseQuery.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmptyResult = Bmb.Core.Domain.Models.EmptyResult;

namespace  Bmb.Corporate.Customer.MasterData.Api.Controllers;

[Route("v1/minhaclasse")]
public class MinhaClasseController : RestControllerBase<MinhaClasseController>
{
    public MinhaClasseController(IMediator bus, INotificationContext notificationContext,
        ILogger<MinhaClasseController> logger) : base(bus, notificationContext, logger) { }

    [HttpPost]
    public async Task<ActionResult<Response<CreateMinhaClasseCommandResult>>> Post([FromBody] CreateMinhaClasseCommand request,
        CancellationToken cancellationToken) =>
        await PostAsync<CreateMinhaClasseCommand, CreateMinhaClasseCommandResult>(request, cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<EmptyResult?>>> Delete([FromRoute] int id, CancellationToken cancellationToken) =>
        await DeleteAsync(new DeleteMinhaClasseCommand(id), cancellationToken);

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<UpdateMinhaClasseCommandResult>>> Put([FromRoute] int id,
    [FromBody] UpdateMinhaClasseCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        return await PutAsync<UpdateMinhaClasseCommand, UpdateMinhaClasseCommandResult>(request, cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ReadOneMinhaClasseQueryResult?>>> GetOne([FromRoute] int id,
        CancellationToken cancellationToken) =>
        await GetAsync<ReadOneMinhaClasseQuery, ReadOneMinhaClasseQueryResult?>(new ReadOneMinhaClasseQuery(id), cancellationToken);

    [HttpGet]
    public async Task<ActionResult<Response<IList<ReadAllMinhaClasseQueryResult>>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] bool onlyActive = true) =>
        await GetAsync<ReadAllMinhaClasseQuery, IList<ReadAllMinhaClasseQueryResult>>(
            new ReadAllMinhaClasseQuery(onlyActive), cancellationToken);

}
