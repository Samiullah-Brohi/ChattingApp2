using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TockenService : ITockenService
{
    private readonly SymmetricSecurityKey _key;
    public TockenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TockenKey"]));
    }
    public string CreateTocken(AppUser user)
    {
        var _claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)

        };

        var _signingCred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var _tockenDescr = new SecurityTokenDescriptor
        {

            Subject = new ClaimsIdentity(_claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = _signingCred

        };

        var _tockenHandler = new JwtSecurityTokenHandler();

        var _tocken = _tockenHandler.CreateToken(_tockenDescr);

        return _tockenHandler.WriteToken(_tocken);
        
    }
}
