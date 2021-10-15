using Expense.Core.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Expense.Core.CQRS.Record.Requests
{
    public class GetAllRecordRequest : IRequest<BaseResponse<List<RecordDto>>>
    {
    }
}
