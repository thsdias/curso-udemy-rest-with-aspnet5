using Microsoft.IdentityModel.Tokens;
using RestWithAspNetUdemy.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNetUdemy.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private TokenConfiguration _configuration;
        
        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = GenerateSymmetricSecurityKey();
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration.Issuer,
                    audience: _configuration.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            SecurityToken securityToken;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GenerateSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                    StringComparison.InvariantCulture))
                throw new SecurityTokenException("Invalid Token");

            return principal;
        }

        private SecurityKey GenerateSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
        }
    }
}
