using MillionRegister.Adapter.MongoDB.Interfaces;
using MillionRegister.Core.Domain.Configuration;
using MongoDB.Driver;

namespace MillionRegister.Adapter.MongoDB.Contexts;

public class FixedIncomeContext : IFixedIncomeContext
{
    public IMongoDatabase Connect()
    {
        var client = new MongoClient(EnvironmentVariables.MongoDb().ConnectionString);

        return client.GetDatabase(EnvironmentVariables.MongoDb().Database);
    }
}