using Expense.API;
using Expense.Core.CQRS.Record.Requests;
using Expense.IntegrationTests.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Expense.IntegrationTests
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;
        private string _url = "http://localhost:5000/api/v1/";

        private CreateRecordRequest _record;
        private API.DTO.CreateCategoryDto _category;

        public ApiTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();

            _category = new API.DTO.CreateCategoryDto()
            {
                Name = "test category",
                Type = "test category type"
            };

            _record = new()
            {
                Amount = 10,
                CategoryId = Guid.NewGuid().ToString(),
                Title = "test record"
            };
        }

        [Theory]
        [InlineData("record")]
        [InlineData("category")]
        public async void GetAll_Should_ReturnOkStatus(string controller)
        {
            //Arrange
            var request = await _httpClient.GetAsync(_url + controller);

            //Assert
            Assert.Equal(HttpStatusCode.OK, request.StatusCode);
        }

        [Fact]
        public async void Record_Post_Should_ReturnNewRecord()
        {
            //Arrange
            var request = await _httpClient.PostAsJsonAsync(_url + "record", _record);

            //Act
            var result = HttpContentHelper<Core.Models.Record>.ReadResult(request).Result;

            //Assert
            Assert.Equal("test record", result.Title);
        }

        [Fact]
        public async void Category_Post_Should_ReturnNewCategory()
        {
            //Arrange
            var request = await _httpClient.PostAsJsonAsync(_url + "category", _category);

            //Act
            var result = HttpContentHelper<API.DTO.CreateCategoryDto>.ReadResult(request).Result;

            //Assert
            Assert.Equal("test category", result.Name);
        }
    }
}
