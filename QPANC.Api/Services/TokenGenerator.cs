using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QPANC.Domain;
using QPANC.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace QPANC.Api.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private QpancContext _context;
        private IJwtBearer _jwtBearer;

        public TokenGenerator(QpancContext context, IJwtBearer jwtBearer)
        {
            this._context = context;
            this._jwtBearer = jwtBearer;
        }

        public async Task<string> Generate(LoginResponse login)
        {
            var roles = await this._context.UserRoles
                .Where(x => x.UserId == login.UserId)
                .Select(x => x.Role.Name)
                .ToListAsync();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, login.SessionId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, login.UserId.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var keySigning = new SymmetricSecurityKey(this._jwtBearer.IssuerSigningKey);
            var signing = new SigningCredentials(keySigning, SecurityAlgorithms.HmacSha256);

            // var keyEncrypting = new SymmetricSecurityKey(this._jwtBearer.TokenDecryptionKey);
            // var encrypting = new EncryptingCredentials(keyEncrypting, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

            var handler = new JwtSecurityTokenHandler();
            handler.OutboundClaimTypeMap.Clear();

            var token = handler.CreateJwtSecurityToken(
                issuer: this._jwtBearer.ValidIssuer,
                audience: this._jwtBearer.ValidAudience,
                subject: new ClaimsIdentity(claims),
                notBefore: DateTime.Now,
                expires: login.ExpiresAt.UtcDateTime,
                issuedAt: DateTime.Now,
                signingCredentials: signing
                // encryptingCredentials: encrypting
            );
            return handler.WriteToken(token);
        }
    }
}
