
using System.Text;
using API.Data;
using API.Interaces;
using API.MyExtensions;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.MyApplicationServices(builder.Configuration);
builder.Services.MyIdentityServices(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();
// new changes
app.UseCors(mycors => mycors.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));

app.UseAuthentication(); // do you have tocken ?
app.UseAuthorization(); // what you have allowed to do

app.MapControllers();

app.Run();
