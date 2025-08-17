using MongoDB.Driver;

namespace MillionRegister.Adapter.MongoDB.Interfaces;

public interface IMongoDbRepository<TEntity>
{
    IMongoCollection<TEntity> GetCollection(string collectionName);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(string id);
    Task AddAsync(TEntity entity);

    Task AddMany(List<TEntity> entity, CancellationToken token);
    Task UpdateAsync(string id, TEntity entity);
    Task DeleteAsync(string id);
}