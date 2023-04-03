using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Users.IntegrationTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UserContext>));
            services.Remove(dbContextDescriptor!);
            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType ==typeof(DbConnection));
            services.Remove(dbConnectionDescriptor!);            

            services.AddDbContext<UserContext>((container, options) =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });            
        });

        builder.UseEnvironment("Development");
    }
}
