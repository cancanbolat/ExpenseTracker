using Expense.Core.Entities;
using System;

namespace Expense.Core.Models
{
    public class Record : BaseEntity
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public Category Category { get; set; }
    }
}
