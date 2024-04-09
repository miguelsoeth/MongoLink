using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoLink.DbSettings;

namespace MongoLink.Services;

public class WeatherForecastService
{
    private readonly IMongoCollection<WeatherForecast> _weatherForecastCollection;

    public WeatherForecastService(IOptions<WeatherForecastSettings> weatherForecastSettings)
    {
        var mongoClient = new MongoClient(weatherForecastSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(weatherForecastSettings.Value.DatabaseName);
        _weatherForecastCollection = mongoDatabase.GetCollection<WeatherForecast>(weatherForecastSettings.Value.WeatherForecastCollectionName);
    }

    public async Task<List<WeatherForecast>> GetAsync() =>
        await _weatherForecastCollection.Find(x => true).ToListAsync();
    
    public async Task<WeatherForecast> GetAsync(string id) =>
        await _weatherForecastCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(WeatherForecast forecast) =>
        await _weatherForecastCollection.InsertOneAsync(forecast);
    public async Task CreateAsync(WeatherForecast[] forecast) =>
        await _weatherForecastCollection.InsertManyAsync(forecast);
    
    public async Task UpdateAsync(string id, WeatherForecast forecast) =>
        await _weatherForecastCollection.ReplaceOneAsync(x=> x.Id == id, forecast);
    
    public async Task removeAsync(string id) =>
        await _weatherForecastCollection.DeleteOneAsync(x=> x.Id == id);
}