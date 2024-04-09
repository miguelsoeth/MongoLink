using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoLink;

public class WeatherForecast
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Date")]
    public DateTime Date { get; set; }
    [BsonElement("TemperatureC")]
    public int TemperatureC { get; set; }
    [BsonElement("TemperatureF")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    [BsonElement("Summary")]
    public string? Summary { get; set; }
}