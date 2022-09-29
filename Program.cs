using MsEFCoreMVC;
using MsEFCoreMVC.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MsEFCoreMVC {
  public class Program {
    public static void Main(string[] args) {
      var builder = WebApplication.CreateBuilder(args);
      var startup = new Startup(builder.Configuration);
      // var host = CreateHostBuilder(args).Build();


      // host.Run();
      // System.Console.WriteLine("Test");

      // Add services to the container.
      builder.Services.AddControllersWithViews();

      startup.ConfigureServices(builder.Services);
      var app = builder.Build();
      // startup.Configure(app, builder.Environment);
      CreateDbIfNotExists(builder.Build());

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

    private static void CreateDbIfNotExists(IHost host) {
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

    public static IHostBuilder CreateHostBuilder(string[] args) {
      return Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<Startup>();
          });
    }
  }
}
