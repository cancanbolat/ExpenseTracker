using Expense.Core.CQRS.Record.Requests;
using Expense.Core.DTOs;
using Expense.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Expense.Core.CQRS.Record.Handlers
{
    public class DeleteAllRecordHandler : IRequestHandler<DeleteAllRecordRequest, BaseResponse<bool>>
    {
        private readonly IRepository<Models.Record> _repository;

        public DeleteAllRecordHandler(IRepository<Models.Record> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteAllRecordRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new();

            try
            {
                await _repository.DeleteAllAsync();
                response.Message = "Deleted successfully";
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
