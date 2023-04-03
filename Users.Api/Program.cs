using ApplicationCore;
using ApplicationCore.DomainServices;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Users.Api.Behaviours;
using Users.Api.CustomFilters;
using Users.Api.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application");
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.    

    builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(new RenderedCompactJsonFormatter()), writeToProviders: true, preserveStaticLogger: true);

    Serilog.Debugging.SelfLog.Enable(Console.Error);
    builder.Services.AddCustomSwagger();
    builder.Services.AddOptions();
    builder.Services.Configure<UsersSetting>(builder.Configuration);
    builder.Logging.AddConsole();
    builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
    builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext")));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddResponseCaching();
    var app = builder.Build();

    app.UseSerilogRequestLogging(options =>
    {
        // Customize the message template
        options.MessageTemplate = "Handled {RequestPath}";

        // Emit debug-level events instead of the defaults
        options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseCustomSwagger();
        using var scope = app.Services.CreateScope();
        var container = scope.ServiceProvider;
        var db = container.GetRequiredService<UserContext>();
        await db.Database.EnsureCreatedAsync();

        if (!await db.Users.AnyAsync())
        {
            try
            {
                await db.InitializeAsync();
            }
            catch (Exception ex)
            {
                container.GetRequiredService<ILogger<Program>>().LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
            }
        }
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseResponseCaching();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapControllers();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

public partial class Program
{

}