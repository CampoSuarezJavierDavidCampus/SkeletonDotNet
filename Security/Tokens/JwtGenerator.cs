using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Contratos;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Security.Tokens;
public class JwtGenerator : IJwtGenerator{
    public string CreateToken(User user){
        var claims = new List<Claim>(){
            new(
                JwtRegisteredClaimNames.NameId,
                user.Usename
            )
        };        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6bz%;MVYYM`6tk2/X3jjC}%CRT#QUUt^Qv$OI<=M?3)wWX+T%-9B'xrt(url%s]Yg@Y9LTsCr}g|e@:>{iSCte}Uwye`"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescription = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(20),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
        
    }
}
