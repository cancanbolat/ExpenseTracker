using Expense.API.Controllers;
using Expense.API.DTO;
using Expense.Core.Interfaces;
using Expense.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Expense.UnitTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<IRepository<Category>> _mock;
        private readonly CategoryController _categoryController;

        public CategoryControllerTests()
        {
            _mock = new Mock<IRepository<Category>>();
            _categoryController = new CategoryController(_mock.Object);
        }

        #region GetAll
        [Fact]
        public async void GetAll_Should_ReturnOkObjectResult()
        {
            //Arrange
            _mock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Core.Models.Category>());

            //Act
            var result = await _categoryController.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_Should_ReturnNotFoundResult()
        {
            //Act
            var result = await _categoryController.GetAll();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Get
        [Fact]
        public async void Get_Should_ReturnOkObjectResult()
        {
            //Arrange
            _mock.Setup(x => x.GetByIdAsync("")).ReturnsAsync(new Core.Models.Category());

            //Act
            var result = await _categoryController.Get("");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get_Should_ReturnNotFoundResult()
        {
            //Act
            var result = await _categoryController.Get("");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Post
        [Fact]
        public async void Post_Should_ReturnCreatedResult()
        {
            //Arrange
            CreateCategoryDto createCategoryDto = new();
            Core.Models.Category createCategory = new();

            _mock.Setup(x => x.CreateAsync(createCategory));

            //Act
            var result = await _categoryController.Post(createCategoryDto);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }
        #endregion

        #region Delete

        #endregion
    }
}
