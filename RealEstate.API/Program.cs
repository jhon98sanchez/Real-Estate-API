using RealEstate.API;
using RealEstate.Repository.SQLServer;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();


        var startup = new Startup(builder.Configuration);

        startup.ConfigureService(builder.Services);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var applicationDBContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
            applicationDBContext.Database.EnsureCreated();
        }

        startup.Configure(app, app.Environment);

        app.Run();
    }
}