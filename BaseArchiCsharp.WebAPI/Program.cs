using System.Reflection;
using BaseArchiCsharp.Application.Common.Behavior;
using BaseArchiCsharp.Infrastructure.Common.Behavior;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BaseArchiCsharp.WebAPI;

public static class Program
{
    // This method add all services and repository in the project
    private static void AddAllServicesRepository(IServiceCollection services)
    {
        // Get classes of the project Application and Infrastructure that have 'service' and 'repository' attribute
        string[] projectNames = ["BaseArchiCsharp.Application", "BaseArchiCsharp.Infrastructure"];
        Assembly[] assemblies = projectNames.Select(Assembly.Load).ToArray();

        // Filter classes depending on classes have 'service' attribute
        List<(Type service, Type[] interfaces)> typeServices = assemblies
            .AsParallel()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttribute<ServiceAttribute>() != null || 
                        t.GetCustomAttribute<RepositoryAttribute>() != null)
            .Select(t => (t, t.GetInterfaces()))
            .ToList();
        
        // Add all services in the container
        foreach ((Type service, Type[] interfaces) in typeServices) {
            foreach (Type @interface in interfaces) {
                services.AddTransient(@interface, service);
            }
        }
    }
    
    // Method to add services, controllers, db, swagger doc and endpoints
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
        });

        IServiceCollection services = builder.Services;
        
        AddAllServicesRepository(services);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseArchiCsharp.WebAPI", Version = "v1" });
            
            // To add XML comments to Swagger
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
    
    // Method to configure the app with middlewares
    private static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseArchiCsharp.WebAPI v1"));
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseArchiCsharp.WebAPI v1"));
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
    
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);
        
        WebApplication app = builder.Build();
        ConfigureApp(app);
        
        await app.RunAsync();
    }
}