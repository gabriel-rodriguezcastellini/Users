using ApplicationCore.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task InitializeAsync()
    {
        await Users.AddRangeAsync(GetSeedingUsers());
        await SaveChangesAsync();
    }

    private static List<User> GetSeedingUsers()
    {
        return new List<User>
        {
            new User( "Juan", "Juan@marmol.com", "Peru 2464", "+5491154762312", UserType.Normal, 1234),
            new User( "Franco", "Franco.Perez@gmail.com", "Alvear y Colombres", "+534645213542", UserType.Premium, 112234),
            new User( "Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534645213543", UserType.SuperUser, 112234)
        };
    }
}
