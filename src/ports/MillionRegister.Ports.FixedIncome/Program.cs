using MillionRegister.Core.Domain.Configuration;
using MillionRegister.Ports.FixedIncome;
using MillionRegister.Ports.FixedIncome.Configuration.DI;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddDependencyInjectionLayer();
builder.Configuration.Initialize();

var host = builder.Build();
host.Run();