using Expense.Core.Entities;
using Expense.Core.Interfaces;
using Expense.Infrastructure.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Expense.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _mongoCollection;
        public Repository(IMongoDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _mongoCollection = db.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAllAsync()
        {
            var status = await _mongoCollection.DeleteManyAsync(new BsonDocument());
            return status.IsAcknowledged && status.DeletedCount > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var status = await _mongoCollection.DeleteOneAsync(x => x.Id == entity.Id);
            return status.IsAcknowledged && status.DeletedCount > 0;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _mongoCollection.Find(x => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>>? filter = null)
        {
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var status = await _mongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return status.IsAcknowledged && status.ModifiedCount > 0;
        }
    }
}
