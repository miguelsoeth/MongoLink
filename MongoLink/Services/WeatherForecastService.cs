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
    
    public async Task<WeatherForecast> GetAsync(DateTime date) =>
        await _weatherForecastCollection.Find(x => x.Date == date).FirstOrDefaultAsync();

    public async Task<WeatherForecast> GetLastAsync(string summary) =>
        await _weatherForecastCollection.Find(x => x.Summary == summary).SortByDescending(x => x.Date).FirstOrDefaultAsync();

    public async Task CreateAsync(WeatherForecast forecast) =>
        await _weatherForecastCollection.InsertOneAsync(forecast);
    public async Task CreateAsync(WeatherForecast[] forecast) =>
        await _weatherForecastCollection.InsertManyAsync(forecast);
    
    public async Task UpdateAsync(DateTime date, WeatherForecast forecast) =>
        await _weatherForecastCollection.ReplaceOneAsync(x=> x.Date == date, forecast);
    
    public async Task removeAsync(DateTime date) =>
        await _weatherForecastCollection.DeleteOneAsync(x=> x.Date == date);
}