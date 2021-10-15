using System;

namespace Expense.Core.DTOs
{
    public class RecordDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CategoryDto Category { get; set; }
    }
}
