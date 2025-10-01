using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Autodealer.Entities;

public class Engine
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    /// <summary>
    /// Объем.
    /// </summary>
    [BsonElement("capacity"), BsonRepresentation(BsonType.Double)]
    public double Capacity { get; set; }
    
    /// <summary>
    /// Количество цилиндров.
    /// </summary>
    [BsonElement("count_block"), BsonRepresentation(BsonType.Int32)]
    public int CountBlock { get; set; }
}