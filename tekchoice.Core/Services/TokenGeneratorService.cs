using tekchoice.Core.Interfaces;
using tekchoice.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace tekchoice.Core.Services
{
    public class TokenGeneratorService : ITokenGenerator
    {
        private readonly AuthenticationDataModel _token;
        public TokenGeneratorService(IOptions<AuthenticationDataModel> token)
        {
            _token = token.Value;
        }
        public string Generator(string email)
        {
            //try
            //{
            //    // appsetting for Token JWT
            //    string secretKey = _token.Secret;
            //    string audienceToken = _token.Audience;
            //    string issuerToken = _token.Issuer;
            //    int expireTime = _token.AccessExpiration;

            //    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            //    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //    // create a claimsIdentity
            //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) });

            //    // create token to the user
            //    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //    JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
            //            audience: audienceToken,
            //            issuer: issuerToken,
            //            subject: claimsIdentity,
            //            notBefore: DateTime.UtcNow,
            //            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
            //            signingCredentials: signingCredentials
            //        );

            //    string jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            //    return jwtTokenString;
            //}
            //catch (Exception exception)
            //{
            //    throw exception;
            //}
            return "";
        }
    }
}
