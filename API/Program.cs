
using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();
// new changes
app.UseCors(mycors => mycors.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("https://localhost:4200"));

app.MapControllers();

app.Run();
