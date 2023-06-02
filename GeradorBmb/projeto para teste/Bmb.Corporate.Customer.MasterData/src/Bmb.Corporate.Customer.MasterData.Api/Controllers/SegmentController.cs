using Bmb.Core.Api.Controllers;
using Bmb.Core.Api.Responses;
using Bmb.Core.Domain.Contracts;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.DeleteSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadAllSegmentQuery.v1;
using Bmb.Corporate.Customer.MasterData.Domain.Segment.Queries.ReadOneSegmentQuery.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EmptyResult = Bmb.Core.Domain.Models.EmptyResult;

namespace Bmb.Corporate.Customer.MasterData.Api.Controllers;

[Route("v1/segment")]
public class SegmentController : RestControllerBase<SegmentController>
{
    public SegmentController(IMediator bus, INotificationContext notificationContext, 
        ILogger<SegmentController> logger) : base(bus, notificationContext, logger) { }
    
    [HttpPost]
    public async Task<ActionResult<Response<CreateSegmentCommandResult>>> Post([FromBody] CreateSegmentCommand request, 
        CancellationToken cancellationToken) => 
        await PostAsync<CreateSegmentCommand, CreateSegmentCommandResult>(request, cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<EmptyResult?>>> Delete([FromRoute] int id, CancellationToken cancellationToken) =>
        await DeleteAsync(new DeleteSegmentCommand(id), cancellationToken);

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<UpdateSegmentCommandResult>>> Put([FromRoute] int id,
        [FromBody] UpdateSegmentCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        
        return await PutAsync<UpdateSegmentCommand, UpdateSegmentCommandResult>(request, cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ReadOneSegmentQueryResult?>>> GetOne([FromRoute] int id,
        CancellationToken cancellationToken) =>
        await GetAsync<ReadOneSegmentQuery, ReadOneSegmentQueryResult?>(new ReadOneSegmentQuery(id), cancellationToken);

    [HttpGet]
    public async Task<ActionResult<Response<IList<ReadAllSegmentQueryResult>>>> GetAll(
        CancellationToken cancellationToken, [FromQuery] bool onlyActive = true) =>
        await GetAsync<ReadAllSegmentQuery, IList<ReadAllSegmentQueryResult>>(
            new ReadAllSegmentQuery(onlyActive), cancellationToken);
}