using Expense.Core.CQRS.Record.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Expense.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _mediator.Send(new GetAllRecordRequest());
            return records != null ? Ok(records) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var record = await _mediator.Send(new GetRecordRequest() { Id = id });
            return record is not null ? Ok(record) : NotFound();
        }

        [HttpGet("[action]/{filter}")]
        public async Task<IActionResult> GetRecordByCategoryName(string filter)
        {
            var request = new GetRecordsByCategoryRequest() { Filter = filter };
            var records = await _mediator.Send(request);
            return records != null ? Ok(records) : NotFound();
        }

        [HttpGet("[action]/{date}")]
        public async Task<IActionResult> GetRecordByDate(string date)
        {
            var request = new GetRecordsByDateRequest() { Date = date };
            var records = await _mediator.Send(request);
            return records != null ? Ok(records) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecordRequest createRecordRequest)
        {
            var result = await _mediator.Send(createRecordRequest);
            return result.Status is true ? Created("", result.Value) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteRecordRequest() { Id = id });
            return result.Status is true ? NoContent() : BadRequest();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAll()
        {
            var result = await _mediator.Send(new DeleteAllRecordRequest());
            return result.Status is true ? NoContent() : BadRequest();
        }

    }
}
