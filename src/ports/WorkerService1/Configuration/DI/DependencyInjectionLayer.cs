using MillionRegister.Adapter.MongoDB.Contexts;
using MillionRegister.Adapter.MongoDB.Entities;
using MillionRegister.Adapter.MongoDB.Interfaces;
using MillionRegister.Adapter.MongoDB.Repository;
using MillionRegister.Core.Application.Interfaces;
using MillionRegister.Core.Application.Services;

namespace WorkerService1.Configuration.DI;

public static class DependencyInjectionLayer
{
    public static void AddDependencyInjectionLayer(this IServiceCollection service)
    {
        service.AddSingleton<IFixedIncomeContext, FixedIncomeContext>();
        service.AddSingleton(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
        service.AddSingleton<IMongoDbRepository<FixedIncome>, MongoDbRepository<FixedIncome>>();
        service.AddTransient<IProcessFixedIncome, ProcessFixedIncome>();
    }
}