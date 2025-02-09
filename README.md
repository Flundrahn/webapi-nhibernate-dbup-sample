# Introduction

The point of doing this project was to learn how to use AspNetCore.Identity with NHibernate.AspNetCore.Identity so can use that instead of the EntityFramework identity stores that are added in the template.

Note that is not the WebApi templates, but rather the different web apps.

# ToDo
[x] Add sample entity `Document`
[ ] Add NHibernate and configure
[ ] Add AspNetCore.Identity
[ ] Add NHibernate.AspNetCore.Identity
[ ] Add identity endpoints

## Authentication and Identity
Corresponding if using Entity Framework would be in `Program.cs`:
```csharp

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

app.MapIdentityApi<IdentityUser>();
```


