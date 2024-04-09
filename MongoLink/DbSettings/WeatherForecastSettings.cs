namespace MongoLink.DbSettings;

public class WeatherForecastSettings
{
    public string? ConnectionString { get; set; } = null;
    public string? DatabaseName { get; set; } = null;
    public string? WeatherForecastCollectionName { get; set; } = null;
}