using Expense.API.Controllers;
using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Expense.UnitTests
{
    public class RecordControllerTests
    {
        private readonly Mock<IMediator> _mock;
        private readonly RecordController _recordController;

        public RecordControllerTests()
        {
            _mock = new Mock<IMediator>();
            _recordController = new RecordController(_mock.Object);
        }

        #region GetAll
        [Fact]
        public async void GetAll_Should_ReturnOkObjectResult()
        {
            //Arragne
            _mock.Setup(x => x.Send(It.IsAny<GetAllRecordRequest>(), default(CancellationToken)))
                .ReturnsAsync(new BaseResponse<List<RecordDto>>());

            //Act
            var result = await _recordController.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_Should_ReturnNotFoundResult()
        {
            //Act
            var result = await _recordController.GetAll();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Get
        [Fact]
        public async void Get_Should_ReturnOkObjectResult()
        {
            //Arragne
            _mock.Setup(x => x.Send(It.IsAny<GetRecordRequest>(), default(CancellationToken)))
                .ReturnsAsync(new BaseResponse<RecordDto>());

            //Act
            var result = await _recordController.Get("");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get_Should_ReturnNotFoundResult()
        {
            //Act
            var result = await _recordController.Get("");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_Should_ReturnCreatedResult()
        {
            //Arrange
            var testRequest = new CreateRecordRequest();

            _mock.Setup(x => x.Send(It.IsIn(testRequest),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<Core.Models.Record>());

            //Act
            var result = await _recordController.Create(testRequest);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void Create_Should_ReturnBadRequestObjectResult()
        {
            //Arrange
            var testRequest = new CreateRecordRequest();

            _mock.Setup(x => x.Send(It.IsIn(testRequest),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<Core.Models.Record>() { Status = false });

            //Act
            var result = await _recordController.Create(testRequest);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete
        [Fact]
        public async void Delete_Should_ReturnNoContentResult()
        {
            //Arrange
            _mock.Setup(x => x.Send(It.Is<DeleteRecordRequest>(x => x.Id == ""),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<bool>());

            //Act
            var result = await _recordController.Delete("");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Delete_Should_ReturnBadRequestResult()
        {
            //Arrange
            _mock.Setup(x => x.Send(It.Is<DeleteRecordRequest>(x => x.Id == ""),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<bool>() { Status = false });

            //Act
            var result = await _recordController.Delete("");

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region DeleteAll
        [Fact]
        public async void DeleteAll_Should_ReturnNoContentResult()
        {
            //Arrange
            _mock.Setup(x => x.Send(It.IsAny<DeleteAllRecordRequest>(),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<bool>());

            //Act
            var result = await _recordController.DeleteAll();

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteAll_Should_ReturnBadRequestResult()
        {
            //Arrange
            _mock.Setup(x => x.Send(It.IsAny<DeleteAllRecordRequest>(),
                default(CancellationToken))).ReturnsAsync(new BaseResponse<bool>() { Status = false });

            //Act
            var result = await _recordController.DeleteAll();

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion
    }
}
