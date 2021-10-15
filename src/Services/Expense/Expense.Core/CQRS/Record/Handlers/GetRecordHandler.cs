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
    public class GetRecordHandler : IRequestHandler<GetRecordRequest, BaseResponse<RecordDto>>
    {
        private readonly IRepository<Models.Record> _repository;
        private readonly IRepository<Models.Category> _categoryRepository;

        public GetRecordHandler(IRepository<Models.Record> repository, IRepository<Core.Models.Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<RecordDto>> Handle(GetRecordRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<RecordDto> response = new();

            try
            {
                var record = await _repository.GetByIdAsync(request.Id);

                record.Category.Name = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Name;
                record.Category.Type = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Type;

                response.Value = LazyMapper.Mapper.Map<RecordDto>(record);
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
