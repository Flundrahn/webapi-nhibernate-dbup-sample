using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NHibernate;
using Shared;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppOptions appOptions)
    {
        services.AddSingleton(sp =>
        {
            var appOptions = sp.GetRequiredService<IOptions<AppOptions>>().Value;
            var nhibernateHelper = new NHibernateHelper(appOptions);
            return nhibernateHelper.CreateSessionFactory();
        });
        services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());

        return services;
    }
}
