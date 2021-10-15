using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class DeleteRecordHandler : IRequestHandler<DeleteRecordRequest, BaseResponse<bool>>
    {
        private readonly IRepository<Models.Record> _repository;

        public DeleteRecordHandler(IRepository<Models.Record> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteRecordRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new();

            try
            {
                var record = await _repository.GetByIdAsync(request.Id);
                await _repository.DeleteAsync(record);
                response.Message = "Record deleted successfully";
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
