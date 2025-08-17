using MillionRegister.Core.Application.Interfaces;

namespace MillionRegister.Ports.FixedIncome;

public class Worker(ILogger<Worker> logger, IProcessFixedIncome process) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Process FixedIncome initialized");
        await process.ExecuteAsync(stoppingToken);
        logger.LogInformation("Process Finished!");
    }
}
