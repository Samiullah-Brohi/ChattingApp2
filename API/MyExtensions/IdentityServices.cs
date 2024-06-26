using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.MyExtensions;

public static class IdentityServices
{
  public static IServiceCollection MyIdentityServices(this IServiceCollection Services, IConfiguration config)
  {
    Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TockenKey"])),
              ValidateIssuer = false,
              ValidateAudience = false
          };
      });

    return Services;
  }
}
