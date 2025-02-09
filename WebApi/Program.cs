using Shared;
using Shared.Exceptions;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// NOTE: To memorize after a quick look at the docs - seems it is possible to name the documents themselves.
// If do that would have to modify the constant OpenApiJsonRoute that I added here
// possible to filter which endpoints should be included
// now the json is generated at each request to the openapi endpoint but it is possible to cache it
// it is possible to limit who is allowed to query the route - unclear so far how to make that work with swagger but might not be a problem

AppOptions appOptions = builder.Configuration.Get<AppOptions>()
    ?? throw new InvalidConfigurationException("Failed to bind AppOptions, please validate configuration of app.");

builder.Services.Configure<AppOptions>(builder.Configuration)
                .AddOpenApi()
                .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (appOptions.IsDevelopment)
{
    app.MapOpenApi(WebApiConstants.OpenApiJsonRoute);
    app.UseSwaggerUI(o => o.SwaggerEndpoint(WebApiConstants.OpenApiJsonRoute, "WebApi"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

