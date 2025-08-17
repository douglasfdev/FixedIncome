using MillionRegister.Core.Application.Interfaces;

namespace WorkerService1;

public class Worker(ILogger<Worker> logger, IProcessFixedIncome process) : BackgroundService
{

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Initialize process FixedIncome");
        await process.ExecuteAsync(stoppingToken);
        logger.LogInformation("Process Finished!");
    }
}
