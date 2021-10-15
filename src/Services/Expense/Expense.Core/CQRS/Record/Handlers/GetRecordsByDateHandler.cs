using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using Expense.Core.Mapping;
using Expense.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class GetRecordsByDateHandler : IRequestHandler<GetRecordsByDateRequest, BaseResponse<List<RecordDto>>>
    {
        private readonly IRepository<Models.Record> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public GetRecordsByDateHandler(IRepository<Core.Models.Record> repository,
            IRepository<Core.Models.Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<List<RecordDto>>> Handle(GetRecordsByDateRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<List<RecordDto>> response = new();
            
            try
            {
                var records = await _repository
                    .GetWhereAsync(x=> x.CreatedAt == request.Date);

                foreach (var record in records)
                {
                    record.Category.Name = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Name;
                    record.Category.Type = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Type;
                }

                response.Value = LazyMapper.Mapper.Map<List<RecordDto>>(records);
                response.Message = $"Count: {records.Count()}";
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
