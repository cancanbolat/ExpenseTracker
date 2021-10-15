using Expense.Core.DTOs;
using MediatR;

namespace Expense.Core.CQRS.Record.Requests
{
    public class GetRecordRequest : IRequest<BaseResponse<RecordDto>>
    {
        public string Id { get; set; }
    }
}
