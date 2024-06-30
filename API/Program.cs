using API.Middleware;
using API.MyExtensions;

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

app.Run();
