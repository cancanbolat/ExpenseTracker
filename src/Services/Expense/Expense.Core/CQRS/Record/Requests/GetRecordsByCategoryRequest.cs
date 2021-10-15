using Expense.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Expense.Core.CQRS.Record.Requests
{
    public class GetRecordsByCategoryRequest : IRequest<BaseResponse<List<RecordDto>>>
    {
        public string Filter { get; set; }
    }
}
