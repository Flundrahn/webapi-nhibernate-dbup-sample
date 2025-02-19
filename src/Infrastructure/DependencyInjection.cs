using Domain.Repositories;
using Infrastructure.Repositories;
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
            // This will return an instance of AppOptions that will work as a singleton, if file changes through app lifetime it will not affect this instance.
            // If GetRequiredService is called within a request scope it would be possible to call
            // var appOptions = sp.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;
            // to fetch an instance of the options that will bind values when it is first accessed, app config could be changed without restart.
            var appOptions = sp.GetRequiredService<IOptions<AppOptions>>().Value;
            var nhibernateHelper = new NHibernateHelper(appOptions);
            return nhibernateHelper.CreateSessionFactory();
        });

        services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession())
                .AddTransient<IDocumentRepository, DocumentRepository>();

        return services;
    }
}
