using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Interfaces;
using hospital__management_system.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace hospital__management_system.EF;
public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration config;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SymmetricSecurityKey key;

    public TokenGenerator(IConfiguration _config, UserManager<ApplicationUser> _userManager)
    {
        this.config = _config;
        userManager = _userManager;
        key = new(Encoding.UTF8.GetBytes(config["JWT:Key"]));
    }

    public async Task<string> GenerateToken(AppUserTokenGeneratorDTO appUserDto)
    {
        ICollection<Claim> claims = GetClaimsList(appUserDto);

        var roles = await userManager.GetRolesAsync(appUserDto.User);

        roles?.ToList().ForEach(role => claims.Add(new(ClaimTypes.Role, role)));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Expires = DateTime.UtcNow.AddDays(double.Parse(config["JWT:ExpInDayes"])),
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
        };

        JwtSecurityTokenHandler tokenHandler = new();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }

    public string GenerateRandomToken()
    {
        return  Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }

    private ICollection<Claim> GetClaimsList(AppUserTokenGeneratorDTO appUserDto)
    {
        return new List<Claim>()
            {
                new(ClaimTypes.Email, appUserDto.Email),
                new(ClaimTypes.NameIdentifier, appUserDto.ID.ToString()),
                new(ClaimTypes.Name, appUserDto.Name),
            };
    }
}
