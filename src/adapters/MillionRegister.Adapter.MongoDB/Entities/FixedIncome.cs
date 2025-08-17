using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionRegister.Adapter.MongoDB.Entities;

public class FixedIncome
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string ExternalId { get; set; }
    
    [BsonElement("OrderId")]
    public string OrderId { get; set; }
    
    [BsonElement("OrderDateTime")]
    public DateTime OrderDateTime { get; set; }
    
    [BsonElement("AssetId")]
    public string AssetId { get; set; }
    
    [BsonElement("TradingAccount")]
    public string TradingAccount { get; set; }
    
    [BsonElement("Quantity")]
    public decimal Quantity { get; set; }
    
    [BsonElement("UnitPrice")]
    public decimal UnitPrice { get; set; }
}