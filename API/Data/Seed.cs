using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
  public static async Task SeedUser(DataContext _context)
  {
    if(await _context.Users.AnyAsync()) return;
    var userData = await File.ReadAllTextAsync("Data/UserDataSeed.json");
    var options =new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
    var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
    foreach (var user in users){

      var hmac = new HMACSHA512();

      user.UserName = user.UserName.ToLower();
      user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@$$w0rd"));
      user.PasswordSalt = hmac.Key;
      _context.Users.Add(user);
      

    }
    await _context.SaveChangesAsync();

  }
}
