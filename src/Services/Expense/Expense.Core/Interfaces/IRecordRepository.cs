using Expense.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense.Core.Interfaces
{
    public interface IRecordRepository : IRepository<Record>
    {
        Task<List<Record>> GetAllRecords(string categoryName);
    }
}
