using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISVENTAS.Infrastructure.Authentication;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
