using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using Expense.Core.Mapping;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class CreateRecordHandler : IRequestHandler<CreateRecordRequest, BaseResponse<Models.Record>>
    {
        private readonly IRepository<Models.Record> _repository;

        public CreateRecordHandler(IRepository<Models.Record> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<Models.Record>> Handle(CreateRecordRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Models.Record>();

            try
            {
                var record = LazyMapper.Mapper.Map<Models.Record>(request);
                var createdRecord = await _repository.CreateAsync(record);

                response.Message = "Record created successfully";
                response.Value = createdRecord;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
