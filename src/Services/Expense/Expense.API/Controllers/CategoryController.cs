using Expense.API.DTO;
using Expense.Core.Interfaces;
using Expense.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Expense.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;

        public CategoryController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();

            return categories is not null ? Ok(categories) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _repository.GetByIdAsync(id);

            return category is not null ? Ok(category) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = new Core.Models.Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = createCategoryDto.Name,
                Type = createCategoryDto.Type
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.CreateAsync(category);
            return Created("", category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category is not null)
            {
                await _repository.DeleteAsync(category);
                return NoContent();
            }

            return NotFound();
        }
    }
}
