using Expense.Core.DTOs;
using MediatR;

namespace Expense.Core.CQRS.Record.Requests
{
    public class UpdateRecordRequest : IRequest<BaseResponse<bool>>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public CategoryDto Category { get; set; }
    }
}
