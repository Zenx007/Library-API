using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Api.Domain.Entities;

namespace TechLibrary.Api.Infrastructure.Security.Tokens.Access;

public class JwtTokenGenerator
{
    public string Generate(User user)
    {
        List<Claim> claims = new List<Claim>()
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    };

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires = DateTime.UtcNow.AddMinutes(60),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken securtyToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securtyToken);
    }

    private SymmetricSecurityKey SecurityKey ()
    {
        var signingKey = "wMyJ530ueaBBqRksp8rpLQnzIafYZHUa";

        var symmetricKey = Encoding.UTF8.GetBytes(signingKey);

        return new SymmetricSecurityKey(symmetricKey);
    }
}
