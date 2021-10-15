using Expense.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Expense.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAllAsync();
        Task<bool> UpdateAsync(string id, T entity);
    }
}
