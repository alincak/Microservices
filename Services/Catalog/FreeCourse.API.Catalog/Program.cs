using FreeCourse.API.Catalog.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace FreeCourse.API.Catalog
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var serviceProvider = scope.ServiceProvider;

        var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

        if (!categoryService.GetAllAsync().Result.Data.Any())
        {
          categoryService.CreateAsync(new Dtos.CategoryDto { Name = "Asp.Net Core Kursu" }).Wait();
          categoryService.CreateAsync(new Dtos.CategoryDto { Name = "Asp.Net Core API Kursu" }).Wait();
        }
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
