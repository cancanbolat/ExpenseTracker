using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using Expense.Core.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class GetRecordsByCategoryHandler : IRequestHandler<GetRecordsByCategoryRequest, BaseResponse<List<RecordDto>>>
    {
        private readonly IRepository<Models.Record> _repository;
        private readonly IRepository<Models.Category> _categoryRepository;

        public GetRecordsByCategoryHandler(IRepository<Core.Models.Record> repository, 
            IRepository<Core.Models.Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<List<RecordDto>>> Handle(GetRecordsByCategoryRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<List<RecordDto>> response = new();

            try
            {
                var records = await _repository.GetAllAsync();

                foreach (var record in records)
                {
                    record.Category.Name = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Name;
                    record.Category.Type = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Type;
                }

                var result = LazyMapper.Mapper.Map<List<RecordDto>>(records);

                response.Value = result.Where(x => x.Category.Name == request.Filter).ToList();

                response.Message = $"Count: {response.Value.Count}";
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
