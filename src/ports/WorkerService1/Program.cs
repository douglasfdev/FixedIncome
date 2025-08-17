using System.Threading.Channels;
using MillionRegister.Core.Application.Dtos;
using MillionRegister.Core.Domain.Configuration;
using WorkerService1;
using WorkerService1.Configuration.DI;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(Channel.CreateUnbounded<RecordBatchDto>());
builder.Services.AddHostedService<Worker>();
builder.Services.AddDependencyInjectionLayer();
builder.Configuration.Initialize();

var host = builder.Build();
host.Run();