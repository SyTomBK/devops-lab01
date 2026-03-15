using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace lab01_hello_api.Services;

public class RedisService
{
    private readonly IDatabase _db;
    public RedisService(IConfiguration config) 
    {
        var redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
        _db = redis.GetDatabase();
    }

    public async Task<string?> GetAsync(string key)
    {
        return await _db.StringGetAsync(key);
    }

    public async Task SetAsync(string key, string value)
    {
        await _db.StringSetAsync(key, value, TimeSpan.FromMinutes(5));
    }

    public async Task DeleteAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}
