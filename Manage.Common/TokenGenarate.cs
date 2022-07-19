﻿using Manage.Model.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common
{
    public class TokenGenarate
    {
        public IConfiguration _configuration { get; }
        public TokenGenarate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(SeUser user)
        {
            var userClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.username),
                new Claim("Password",user.password),
                new Claim("ID",$"{user.Id}"),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(userClaim);
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var secretkeyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var accessToken = jwtTokenHandle.CreateToken(tokenDescription);
            return jwtTokenHandle.WriteToken(accessToken);
        }
    }
}
