using System.Threading.Channels;
using MillionRegister.Adapter.MongoDB.Contexts;
using MillionRegister.Adapter.MongoDB.Interfaces;
using MillionRegister.Adapter.MongoDB.Repository;
using MillionRegister.Core.Application.Dtos;
using MillionRegister.Core.Application.Interfaces;
using MillionRegister.Core.Application.Services;

namespace MillionRegister.Ports.FixedIncome.Configuration.DI;

public static class DependencyInjectionLayer
{
    public static void AddDependencyInjectionLayer(this IServiceCollection service)
    {
        service.AddSingleton(Channel.CreateUnbounded<RecordBatchDto>());
        service.AddSingleton<IFixedIncomeContext, FixedIncomeContext>();
        service.AddSingleton(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
        service.AddSingleton<IMongoDbRepository<Adapter.MongoDB.Entities.FixedIncome>, MongoDbRepository<Adapter.MongoDB.Entities.FixedIncome>>();
        service.AddTransient<IProcessFixedIncome, ProcessFixedIncome>();
    }
}