using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using Expense.Core.Mapping;
using Expense.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class GetAllRecordHandler : IRequestHandler<GetAllRecordRequest, BaseResponse<List<RecordDto>>>
    {
        private readonly IRepository<Models.Record> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public GetAllRecordHandler(IRepository<Models.Record> repository, 
            IRepository<Models.Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<List<RecordDto>>> Handle(GetAllRecordRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<List<RecordDto>> response = new();
            
            try
            {
                var records = await _repository.GetAllAsync();

                //record category
                foreach (var record in records)
                {
                    record.Category.Name = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Name;
                    record.Category.Type = _categoryRepository.GetByIdAsync(record.Category.Id).Result.Type;
                }

                response.Value = LazyMapper.Mapper.Map<List<RecordDto>>(records);
                response.Message = $"Count: {records.Count} \n Total Amount: {records.Sum(x => x.Amount)}";
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
