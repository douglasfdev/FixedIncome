using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionRegister.Adapter.MongoDB.Entities;

public class FixedIncome
{
    [BsonConstructor]
    public FixedIncome(
        string externalId,
        string orderId,
        DateTime orderDateTime,
        string assetId,
        string tradingAccount,
        decimal quantity,
        decimal unitPrice
    )
    {
        ExternalId = externalId;
        OrderId = orderId;
        OrderDateTime = orderDateTime;
        AssetId = assetId;
        TradingAccount = tradingAccount;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string ExternalId { get; }

    [BsonElement("OrderId")]
    public string OrderId { get; }

    [BsonElement("OrderDateTime")]
    public DateTime OrderDateTime { get; }
    
    [BsonElement("AssetId")]
    public string AssetId { get; }
    
    [BsonElement("TradingAccount")]
    public string TradingAccount { get; }
    
    [BsonElement("Quantity")]
    public decimal Quantity { get; }
    
    [BsonElement("UnitPrice")]
    public decimal UnitPrice { get; }
    
    public static implicit operator Core.Domain.Entities.FixedIncome(FixedIncome entity)
        => new (
            entity.ExternalId,
            entity.OrderId,
            entity.OrderDateTime,
            entity.AssetId,
            entity.TradingAccount,
            entity.Quantity,
            entity.UnitPrice
        );
}