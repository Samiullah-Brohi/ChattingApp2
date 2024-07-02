using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using API.MyExtensions;

namespace API.Entities;

public class AppUser
{
  public int Id { get; set; }
  public required string UserName { get; set; }
  public byte[] PasswordHash { get; set; }
  public byte[] PasswordSalt { get; set; }
  public DateOnly DateOfBirth { get; set; }
  public String KnownAs { get; set; }
  public DateTime Created { get; set; } = DateTime.UtcNow;
  public DateTime LastActive { get; set; } = DateTime.UtcNow;
  public String Gender  { get; set; }
  public String Introduction { get; set; }  
  public String LookingFor    { get; set; }
  public String Interests { get; set; } 
  public String City { get; set; }  
  public String Country { get; set; }
  public List<Photo> Photos { get; set; } = new();

 

  // public int GetAge()
  // {
  //   return DateOfBirth.CalculateAge();
  // }

}

