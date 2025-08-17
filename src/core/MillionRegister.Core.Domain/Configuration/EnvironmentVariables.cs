using Microsoft.Extensions.Configuration;
using MillionRegister.Core.Common;

namespace MillionRegister.Core.Domain.Configuration;

public static class EnvironmentVariables
{
    private static IConfiguration? _configuration;

    public static void Initialize(this IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public static MongoDbEnvironments MongoDb()
    {
        if (_configuration == null)
            throw new InvalidOperationException("Configuration not initialized.");

        var settings = _configuration.GetSection("MongoDb").Get<MongoDbEnvironments>();
        return Validate(settings ?? throw new InvalidOperationException("MongoDb settings not found."));
    }
    public static string UploadDirectory() => _configuration?.GetSection("UploadDirectory").Value ?? string.Empty;

    private static MongoDbEnvironments Validate(MongoDbEnvironments mongoDbEnvironments)
    {
        Utils.ThrowIfIsNullOrEmpty(mongoDbEnvironments.ConnectionString, nameof(MongoDbEnvironments.ConnectionString));
        Utils.ThrowIfIsNullOrEmpty(mongoDbEnvironments.Database, nameof(MongoDbEnvironments.Database));
        Utils.ThrowIfIsNullOrEmpty(mongoDbEnvironments.Username, nameof(MongoDbEnvironments.Username));
        Utils.ThrowIfIsNullOrEmpty(mongoDbEnvironments.Password, nameof(MongoDbEnvironments.Password));
        return mongoDbEnvironments;
    }
}