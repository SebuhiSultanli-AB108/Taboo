using Microsoft.AspNetCore.Diagnostics;
using Taboo.Enums;
using Taboo.Exceptions;
using Taboo.ExternalServices.Abstracts;
using Taboo.ExternalServices.Implements;
using Taboo.Services.Abstracts;
using Taboo.Services.Implements;

namespace Taboo;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IBannedWordService, BannedWordService>();
        return services;
    }
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration _configuration, CacheTypes type = CacheTypes.Redis)
    {
        if (type == CacheTypes.Redis)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = _configuration.GetConnectionString("Redis");
                opt.InstanceName = "_Taboo";
            });
            services.AddScoped<ICacheService, CacheService>();
        }
        if (type == CacheTypes.Local)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService, LocalCacheService>();
        }
        return services;
    }
    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                Exception exc = context.Features.Get<IExceptionHandlerFeature>()!.Error;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                if (exc is IBaseException ibe)
                {
                    context.Response.StatusCode = ibe.StatusCode;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ibe.StatusCode,
                        Message = ibe.ErrorMessage
                    });
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Something went wrong!"
                    });
                }
            });
        });
        return app;
    }
}
