using Microsoft.EntityFrameworkCore;
using lab01_hello_api.Models;

namespace lab01_hello_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}