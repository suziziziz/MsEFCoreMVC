using MsEFCoreMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MsEFCoreMVC {
  public class Startup {
    public IConfiguration _configuration { get; }

    public Startup(IConfiguration configuration) {
      _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<SchoolContext>(options =>
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment) {

    }
  }
}
