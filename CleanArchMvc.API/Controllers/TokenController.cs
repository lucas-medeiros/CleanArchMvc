using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private const int EXPIRATION_TIME = 10;

    private readonly IAuthenticate _authentication;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authentication, IConfiguration configuration)
    {
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        _configuration = configuration;
    }

    [HttpPost("CreateUser")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public async Task<ActionResult> Createuser([FromBody] LoginModel userInfo)
    {
        var result = await _authentication.RegisterUserAsync(userInfo.Email, userInfo.Password);
        if (result)
        {
            return Ok($"User {userInfo.Email} was create successfully");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Register Attempt");
            return BadRequest(ModelState);
        }
    }

    [AllowAnonymous]
    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
    {
        var result = await _authentication.AuthenticateAsync(userInfo.Email, userInfo.Password);
        if (result)
        {
            return GenerateToken(userInfo);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return BadRequest(ModelState);
        }
    }

    private UserToken GenerateToken(LoginModel userInfo)
    {
        // Declarações de usuário
        var claims = new[]
        {
            new Claim("email", userInfo.Email),
            new Claim("role", "user"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //Gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        //Gerar assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        //Definir tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_TIME);

        //Gerar Token
        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
