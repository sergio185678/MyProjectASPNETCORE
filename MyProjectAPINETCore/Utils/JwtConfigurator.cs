using Microsoft.IdentityModel.Tokens;
using MyProjectAPINETCore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProjectAPINETCore.Utils
{
    public class JwtConfigurator
    {
        public static string GetToken(Usuario usuarioInfo, IConfiguration configuration)
        {
            string Secretkey = configuration["Jwt:SecretKey"];
            string Issuer = configuration["Jwt:Issuer"];
            string Audience = configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usuarioInfo.NombreCompleto),
                new Claim("idUsuario",usuarioInfo.UserId.ToString()),
                new Claim("cargo",usuarioInfo.oCargo.Nombre)
            };
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims,
                expires: DateTime.Now.AddDays(1),//tiempo que el token dure(1dia)
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int GetTokenIdUsuario(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "idUsuario")
                    {
                        return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }
        public static string GetTokenSubject(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                var subClaim = identity.FindFirst(ClaimTypes.NameIdentifier); 

                if (subClaim != null)
                {
                    var subValue = subClaim.Value;
                    return subValue;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        public static string GetTokenCargo(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "cargo")
                    {
                        return claim.Value;
                    }
                }
            }
            return null;
        }
    }
}
