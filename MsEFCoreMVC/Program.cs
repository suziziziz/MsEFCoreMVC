using MsEFCoreMVC;
using MsEFCoreMVC.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace MsEFCoreMVC {
  public class Program {
    public static void Main(string[] args) {
      var builder = WebApplication.CreateBuilder(args);
      var startup = new Startup(builder.Configuration);

      startup.ConfigureServices(builder.Services);

      // Add services to the container.
      builder.Services.AddControllersWithViews();

      var app = builder.Build();

      startup.Configure(app, app.Environment);

      CreateDbIfNotExists(app);

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");

      app.Run();
    }

    private static void CreateDbIfNotExists(WebApplication host) {
      using (var scope = host.Services.CreateScope()) {
        var services = scope.ServiceProvider;
        try {
          var context = services.GetRequiredService<SchoolContext>();
          DbInitializer.Initialize(context);
        }
        catch (Exception ex) {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred creating the DB.");
        }
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
              webBuilder.UseStartup<Startup>();
            });
  }
}
