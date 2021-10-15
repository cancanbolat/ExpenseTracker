using Expense.Core.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.Core.CQRS.Record.Requests
{
    public class CreateRecordRequest : IRequest<BaseResponse<Models.Record>>
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [Range(1, 999999999)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MinLength(1)]
        public string CategoryId { get; set; }
    }
}
