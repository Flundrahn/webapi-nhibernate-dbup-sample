using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Shared;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddSingleton(sp => new NHibernateHelper(appOptions).CreateSessionFactory())
                .AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

        return services;
    }
}
