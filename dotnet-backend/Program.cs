using dotnet_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext with MySQL connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36))
                ));

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure middleware
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        // This is important for EF Core tools at design-time
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<StartupDummy>();
        //        });
    }

    // Dummy Startup just so EF Core tools can build the DbContext
    //public class StartupDummy
    //{
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        // EF Tools will use Program's configuration anyway
    //    }

    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //    }
    //}
}
