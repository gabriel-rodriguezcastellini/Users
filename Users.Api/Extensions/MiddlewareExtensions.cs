using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Users.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Users API",
                Version = "v1",
                Description = "Users RESTful API built with .NET 7.",
                Contact = new OpenApiContact
                {
                    Name = "Gabriel Rodríguez Castellini",
                    Url = new Uri("https://github.com/gabriel-rodriguezcastellini")
                }
            });

            cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger().UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API");
            options.DocumentTitle = "Users API";
        });

        return app;
    }
}
