using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public class ApiContext : DbContext
{
    protected ApiContext(DbContextOptions<ApiContext> options): base(options){}
    public DbSet<Rol> Rols { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RolUser> RolUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {            
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
