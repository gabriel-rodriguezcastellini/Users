using ApplicationCore.Entities.UserAggregate;
using Infrastructure;

namespace Users.IntegrationTests.Helpers;

public static class Utilities
{
    public static async Task InitializeDbForTestsAsync(UserContext db)
    {
        await db.Users.AddRangeAsync(GetSeedingUsers());
        await db.SaveChangesAsync();
    }

    public static async Task ReinitializeDbForTestsAsync(UserContext db)
    {
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
        db.Users.RemoveRange(db.Users);
        await InitializeDbForTestsAsync(db);
    }

    private static List<User> GetSeedingUsers()
    {
        return new List<User>
        {
            new User("Juan", "Juan@marmol.com", "Peru 2464", "+5491154762312", UserType.Normal, 1234),
            new User("Franco", "Franco.Perez@gmail.com", "Alvear y Colombres", "+534645213542", UserType.Premium, 112234),
            new User("Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534645213543", UserType.SuperUser, 112234)
        };
    }
}
