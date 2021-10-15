using System;
using System.ComponentModel.DataAnnotations;

namespace Expense.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
    }
}
