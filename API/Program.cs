using API;
using API.Data;
using API.Middleware;
using API.MyExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.MyApplicationServices(builder.Configuration);
builder.Services.MyIdentityServices(builder.Configuration);
builder.Services.AddCors();

var app = builder.Build();
// custom middleware for exception handling
app.UseMiddleware<ExceptionsMiddleware>();

// new changes
app.UseCors(mycors => mycors.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));

app.UseAuthentication(); // do you have tocken ?
app.UseAuthorization(); // what you have allowed to do

app.MapControllers();
// use seeding data if there is no data in the database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try{
  var _context = services.GetRequiredService<DataContext>();
  await _context.Database.MigrateAsync();
  await Seed.SeedUser(_context);

}
catch(Exception ex) {
  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex.Message,"an error ocuured during migration in program.cs file");
 }



app.Run();
