namespace MillionRegister.Core.Application.Interfaces;

public interface IProcessFixedIncome
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}