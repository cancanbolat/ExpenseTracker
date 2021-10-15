using Expense.Core.DTOs;
using MediatR;

namespace Expense.Core.CQRS.Record.Requests
{
    public class DeleteAllRecordRequest : IRequest<BaseResponse<bool>>
    {
    }
}
