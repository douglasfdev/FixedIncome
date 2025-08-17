using System.Diagnostics;
using System.Reflection;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using MillionRegister.Adapter.MongoDB.Interfaces;
using MillionRegister.Core.Application.Dtos;
using MillionRegister.Core.Application.Interfaces;
using MillionRegister.Core.Domain.Entities;

namespace MillionRegister.Core.Application.Services;

public class ProcessFixedIncome(
    ILogger<ProcessFixedIncome> logger,
    Channel<RecordBatchDto> channel,
    IMongoDbRepository<FixedIncome> repository
) : IProcessFixedIncome
{
    private readonly ChannelReader<RecordBatchDto> _channelReader = channel.Reader;
    private readonly ChannelWriter<RecordBatchDto> _channelWriter = channel.Writer;
    private readonly SemaphoreSlim _mongoSemaphore = new(5);
    private bool _headerWritten = false;

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("MillionRegister.Core.Application.FixedIncomeData.dados.csv");
        
        if (stream == null)
        {
            logger.LogError("Embedded resource {resource} not found", stream?.ToString());
            return;
        }

        logger.LogInformation("Starting processing of {file}", stream.GetType().Name);

        var producer = Task.Run(() => ReadCsvFile(stream, stoppingToken));

        var consumers = Enumerable.Range(0, Environment.ProcessorCount)
            .Select(_ => Task.Run(() => ProcessBatches(stoppingToken)))
            .ToArray();

        await Task.WhenAll(producer);
        _channelWriter.Complete();
        await Task.WhenAll(consumers);
    }
    
    private async Task ReadCsvFile(Stream csvStream, CancellationToken stoppingToken)
    {
        const int batchSize = 10_000;
        var batch = new List<FixedIncome>(batchSize);

        using var reader = new StreamReader(csvStream);
        string? header = await reader.ReadLineAsync(stoppingToken);

        while (!reader.EndOfStream && !stoppingToken.IsCancellationRequested)
        {
            string? line = await reader.ReadLineAsync(stoppingToken);
            if (line == null) continue;

            var cols = line.Split(',');

            var dto = new FixedIncomeDto(
                cols[0],
                cols[1],
                DateTime.Parse(cols[2]),
                cols[3],
                cols[4],
                decimal.Parse(cols[5]),
                decimal.Parse(cols[6])
            );

            batch.Add(dto);

            if (batch.Count >= batchSize)
            {
                await _channelWriter.WriteAsync(new RecordBatchDto { Batch = batch.ToList() }, stoppingToken);
                batch.Clear();
            }
        }

        if (batch.Count > 0)
            await _channelWriter.WriteAsync(new RecordBatchDto { Batch = batch }, stoppingToken);
    }

    private async Task ProcessBatches(CancellationToken stoppingToken)
    {
        logger.LogInformation("Begin save");
        var stopwatch = Stopwatch.StartNew();
        await foreach (var item in _channelReader.ReadAllAsync(stoppingToken))
        {
            var fixedIncomes = item.Batch;

            try
            {
                await _mongoSemaphore.WaitAsync(stoppingToken);
                try
                {
                    await repository.AddMany(fixedIncomes, token: stoppingToken);
                }
                finally
                {
                    _mongoSemaphore.Release();
                }

                foreach (var record in item.Batch)
                {
                    if (CheckDiscrepancy(record))
                    {
                        logger.LogWarning("Discrepancy found for {externalId}", record.ExternalId);
                        //await WriteDiscrepancyAsync(record);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error saving batch to MongoDB");
            }
        }

        logger.LogInformation("Finish save");
        stopwatch.Stop();
        logger.LogInformation("Processing completed in {elapsed} seconds", stopwatch.Elapsed.TotalSeconds);
    }

    private bool CheckDiscrepancy(FixedIncome record)
    {
        return record.Quantity <= 0;
    }
    
    // private async Task WriteDiscrepancyAsync(FixedIncome record)
    // {
    //     await _csvSemaphore.WaitAsync();
    //     try
    //     {
    //         bool writeHeader = false;
    //         if (!_headerWritten && !File.Exists(Path.Combine(_uploadDirectory, "discrepancy_report.csv")))
    //         {
    //             writeHeader = true;
    //             _headerWritten = true;
    //         }
    //
    //         using var stream = new FileStream(Path.Combine(_uploadDirectory, "discrepancy_report.csv"), FileMode.Append, FileAccess.Write, FileShare.Read);
    //         using var writer = new StreamWriter(stream);
    //
    //         if (writeHeader)
    //             await writer.WriteLineAsync("ExternalId,OrderId,OrderDateTime,AssetId,TradingAccount,Quantity,UnitPrice");
    //
    //         await writer.WriteLineAsync($"{record.ExternalId},{record.OrderId},{record.OrderDateTime:O},{record.AssetId},{record.TradingAccount},{record.Quantity},{record.UnitPrice}");
    //     }
    //     finally
    //     {
    //         _csvSemaphore.Release();
    //     }
    // }
}