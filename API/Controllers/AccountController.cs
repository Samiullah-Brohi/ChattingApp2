using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Interaces;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITockenService _tockenService;

    public AccountController(DataContext context, ITockenService tockenService)
    {
        _context = context;
        _tockenService = tockenService;
    }
    [HttpPost("register")]
    //Register(String username, String password) using query string to send data
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO) 
    { 
        if(await UserExists(registerDTO.UserName))
        {
            return BadRequest("User is already tacken");

        } 

      using var hmac = new HMACSHA256();

      var user = new AppUser {
         
          UserName = registerDTO.UserName.ToLower(),
          PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
          PasswordSalt = hmac.Key

      };
       _context.Users.Add(user);
       await _context.SaveChangesAsync();

       return new UserDTO
       {
         UserName = user.UserName,
         Tocken = _tockenService.CreateTocken(user)
       };

    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await  _context.Users.SingleOrDefaultAsync(x=> x.UserName == loginDTO.UserName.ToLower());
        if(user == null) return Unauthorized("Invalid User");

        var hmac = new HMACSHA256(user.PasswordSalt);
        var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
        
        for(int i = 0; i< ComputedHash.Length; i++)
        {
           if(ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDTO
        {
            UserName = user.UserName,
            Tocken = _tockenService.CreateTocken(user)
        };

    }


    private async Task<bool> UserExists(String Username)
    {
        return await _context.Users.AnyAsync(u => u.UserName == Username.ToLower());

    }
    
}
