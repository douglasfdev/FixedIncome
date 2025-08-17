using MongoDB.Driver;

namespace MillionRegister.Adapter.MongoDB.Interfaces;

public interface IFixedIncomeContext
{
    IMongoDatabase Connect();
}