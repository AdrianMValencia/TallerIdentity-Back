using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISVENTAS.Infrastructure.Authentication;
using System.Reflection;
using TallerIdentity.Application.Interfaces.Authentication;
using TallerIdentity.Application.Interfaces.Persistence;
using TallerIdentity.Application.Interfaces.Services;
using TallerIdentity.Infrastructure.Authentication;
using TallerIdentity.Infrastructure.Persistence.Context;
using TallerIdentity.Infrastructure.Persistence.Repositories;
using TallerIdentity.Infrastructure.Services;

namespace TallerIdentity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("TallerIdentityConnection"),
                x => x.MigrationsAssembly(assembly)));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        var infraAsm = Assembly.GetExecutingAssembly();
        foreach (var impl in infraAsm.GetTypes()
                     .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository")))
        {
            var @interface = impl.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + impl.Name);
            if (@interface != null)
                services.AddScoped(@interface, impl);
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
