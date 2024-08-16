using login.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace login.Infrastructure.Data;

public class Database(DbContextOptions<Database> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}