using lab01_hello_api.Data;
using lab01_hello_api.Models;
using lab01_hello_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace lab01_hello_api.Controllers;

[Route("users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _db;
    private readonly RedisService _redis;
    public UserController(AppDbContext db, ILogger<UserController> logger, RedisService redis)
    {
        _logger = logger;
        _db = db;
        _redis = redis;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cache = await _redis.GetAsync("users");

        if (cache != null)
        {
            _logger.LogInformation("Cache hit");
            var users = JsonSerializer.Deserialize<List<User>>(cache);
            return Ok(users);
        }

        _logger.LogInformation("Cache miss");

        var data = await _db.Users.ToListAsync();
        await _redis.SetAsync("users", JsonSerializer.Serialize(data));

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        user.Id = Guid.NewGuid();

        _db.Users.Add(user);

        await _db.SaveChangesAsync();
        await _redis.DeleteAsync("users");
        return Ok(user);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var user = UserStore.Users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var user = UserStore.Users.FirstOrDefault(x => x.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        UserStore.Users.Remove(user);
        return NoContent();
    }
}
