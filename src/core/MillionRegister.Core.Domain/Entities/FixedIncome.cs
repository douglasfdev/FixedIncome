namespace MillionRegister.Core.Domain.Entities;

public class FixedIncome(
    string? externalId,
    string? orderId,
    DateTime orderDateTime,
    string? assetId,
    string? tradingAccount,
    decimal quantity,
    decimal unitPrice
)
{
    public string? ExternalId { get; } = externalId;
    public string? OrderId { get; } = orderId;
    public DateTime OrderDateTime { get; } = orderDateTime;
    public string? AssetId { get; } = assetId;
    public string? TradingAccount { get; } = tradingAccount;
    public decimal Quantity { get; } = quantity;
    public decimal UnitPrice { get; } = unitPrice;
}