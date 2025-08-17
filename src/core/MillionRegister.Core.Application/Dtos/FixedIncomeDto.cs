using MillionRegister.Core.Domain.Entities;

namespace MillionRegister.Core.Application.Dtos;

public class FixedIncomeDto(
    string? externalId,
    string? orderId,
    DateTime orderDateTime,
    string? assetId,
    string? tradingAccount,
    decimal quantity,
    decimal unitPrice
)
{
    private string? ExternalId { get; } = externalId;
    private string? OrderId { get; } = orderId;
    private DateTime OrderDateTime { get; } = orderDateTime;
    private string? AssetId { get; } = assetId;
    private string? TradingAccount { get; } = tradingAccount;
    private decimal Quantity { get; } = quantity;
    private decimal UnitPrice { get; } = unitPrice;
    
    public static implicit operator FixedIncome(FixedIncomeDto fixedIncomeDto)
    {
        return MapToFixedIncome(fixedIncomeDto);
    }

    private static FixedIncome MapToFixedIncome(FixedIncomeDto fixedIncome)
        => new(
            fixedIncome.ExternalId,
            fixedIncome.OrderId,
            fixedIncome.OrderDateTime,
            fixedIncome.AssetId,
            fixedIncome.TradingAccount,
            fixedIncome.Quantity, fixedIncome.UnitPrice
        );
}