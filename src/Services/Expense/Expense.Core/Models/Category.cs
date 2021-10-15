using Expense.Core.Entities;

namespace Expense.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
