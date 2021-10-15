using AutoMapper;
using Expense.Core.CQRS.Record.Handlers;
using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Expense.UnitTests
{
    public class CqrsTests
    {
        private readonly Mock<IRepository<Core.Models.Record>> _mockRecord;
        private readonly Mock<IRepository<Core.Models.Category>> _mockCategory;
        private List<Core.Models.Record> _records;

        public CqrsTests()
        {
            _mockRecord = new Mock<IRepository<Core.Models.Record>>();
            _mockCategory = new Mock<IRepository<Core.Models.Category>>();

            _records = new List<Core.Models.Record>()
            {
                new Core.Models.Record{
                    Id = "id1",
                    Title = "Record 1",
                    Amount = 10,
                    Category = new Core.Models.Category
                    {
                        Id = "catId1",
                        Name = "category 1",
                        Type = "income"
                    }
                },
                new Core.Models.Record{
                    Id = "id2",
                    Title = "Record 2",
                    Amount = 20,
                    Category = new Core.Models.Category
                    {
                        Id = "catId1",
                        Name = "category 1",
                        Type = "income"
                    }
                },
            };
        }

        #region GetAll & Get Record
        [Fact]
        public async void GetAllRecordHandler_Should_ReturnRecords()
        {
            //Arrange
            _mockRecord.Setup(x => x.GetAllAsync()).ReturnsAsync(_records);
            _mockCategory.Setup(x => x.GetByIdAsync("catId1")).ReturnsAsync(_records[0].Category);

            var handler = new GetAllRecordHandler(_mockRecord.Object, _mockCategory.Object);

            /*
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<Record>>(It.IsAny<BaseResponse<List<RecordDto>>>()));
            */

            //Act
            var result = await handler.Handle(new GetAllRecordRequest(), default(CancellationToken));

            //Assert
            Assert.Equal(2, result.Value.Count);
            Assert.Equal("Record 1", result.Value[0].Title);
            Assert.Equal("category 1", result.Value[0].Category.Name);
        }

        [Theory]
        [InlineData("id1")]
        public async void GetRecordHandler_Should_ReturnRecord(string id)
        {
            //Arrange
            _mockRecord.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(_records.First());
            _mockCategory.Setup(x => x.GetByIdAsync("catId1")).ReturnsAsync(_records[0].Category);

            var handler = new GetRecordHandler(_mockRecord.Object, _mockCategory.Object);

            //Act
            var result = await handler.Handle(new GetRecordRequest() { Id = id }, default(CancellationToken));

            //Assert
            Assert.Equal("Record 1", result.Value.Title);
        }

        [Fact]
        public async void GetRecordHandler_Should_ReturnNull()
        {
            //Arrange
            var handler = new GetRecordHandler(_mockRecord.Object, _mockCategory.Object);

            //Act
            var result = await handler.Handle(new GetRecordRequest(), default(CancellationToken));

            //Assert
            Assert.Null(result.Value);
        }
        #endregion

        #region Create & Delete & Update Record
        [Fact]
        public async void CreateRecordHandler_Should_MockVerify()
        {
            //Arrange
            var handler = new CreateRecordHandler(_mockRecord.Object);

            //Act
            var result = await handler.Handle(new CreateRecordRequest(), default(CancellationToken));

            //Assert
            _mockRecord.Verify(x => x.CreateAsync(It.IsAny<Core.Models.Record>()), Times.Once);
        }

        [Fact]
        public async void DeleteRecordHandler_Should_MockVerify()
        {
            //Arrange
            var handler = new DeleteRecordHandler(_mockRecord.Object);

            //Act
            var result = await handler.Handle(new DeleteRecordRequest(), default(CancellationToken));

            //Assert
            _mockRecord.Verify(x => x.DeleteAsync(It.IsAny<Core.Models.Record>()), Times.Exactly(1));
        }

        [Fact]
        public async void DeleteAllRecordHandler_Should_MockVerify()
        {
            //Arrange
            var handler = new DeleteAllRecordHandler(_mockRecord.Object);

            //Act
            var result = await handler.Handle(new DeleteAllRecordRequest(), default(CancellationToken));

            //Assert
            _mockRecord.Verify(x => x.DeleteAllAsync(), Times.Once);
        }
        #endregion
    }
}
