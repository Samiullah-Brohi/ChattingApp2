using API.Data;
using API.Interaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.MyExtensions;

public static class ApplicationServiceExtensions
{
  public static IServiceCollection MyApplicationServices(this IServiceCollection Services, IConfiguration config)
  {
    Services.AddDbContext<DataContext>(options =>
    {
      options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });

    Services.AddScoped<ITockenService, TockenService>();
    Services.AddScoped<IUsersRepository, UsersRepository>();
    Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    
    return Services;
  }
}
