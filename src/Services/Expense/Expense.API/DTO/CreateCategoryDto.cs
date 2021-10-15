using System;
using System.ComponentModel.DataAnnotations;

namespace Expense.API.DTO
{
    public class CreateCategoryDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        public string Type { get; set; }
    }
}
