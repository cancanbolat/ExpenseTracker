using Expense.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace Expense.Core.CQRS.Record.Requests
{
    public class GetRecordsByDateRequest : IRequest<BaseResponse<List<RecordDto>>>
    {
        public string Date { get; set; }
    }
}
