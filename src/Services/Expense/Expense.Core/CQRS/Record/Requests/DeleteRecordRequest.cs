using Expense.Core.DTOs;
using MediatR;

namespace Expense.Core.CQRS.Record.Requests
{
    public class DeleteRecordRequest : IRequest<BaseResponse<bool>>
    {
        public string Id { get; set; }
    }
}
