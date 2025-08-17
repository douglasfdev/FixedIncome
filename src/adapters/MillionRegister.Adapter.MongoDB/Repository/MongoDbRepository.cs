using MillionRegister.Adapter.MongoDB.Contexts;
using MillionRegister.Adapter.MongoDB.Interfaces;
using MongoDB.Driver;

namespace MillionRegister.Adapter.MongoDB.Repository;

public class MongoDbRepository<TEntity>(IFixedIncomeContext context) : IMongoDbRepository<TEntity>
{
    public IMongoCollection<TEntity> GetCollection(string collectionName)
        => context.Connect().GetCollection<TEntity>(collectionName);

    public async Task<List<TEntity>> GetAllAsync()
        => await GetCollection(typeof(TEntity).Name).Find(_ => true).ToListAsync();

    public async Task<TEntity?> GetByIdAsync(string id)
        => await GetCollection(typeof(TEntity).Name)
            .Find(Builders<TEntity>.Filter.Eq("_id", id))
            .FirstOrDefaultAsync();

    public async Task AddAsync(TEntity entity)
        => await GetCollection(typeof(TEntity).Name).InsertOneAsync(entity);

    public async Task AddMany(List<TEntity> entity, CancellationToken token)
        => await GetCollection(typeof(TEntity).Name).InsertManyAsync(entity, cancellationToken: token);

    public async Task UpdateAsync(string id, TEntity entity)
        => await GetCollection(typeof(TEntity).Name)
            .ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity);

    public async Task DeleteAsync(string id)
        => await GetCollection(typeof(TEntity).Name)
            .DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
}