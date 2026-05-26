using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repo;
    private readonly IConfiguration _config;

    public AuthService(IUsuarioRepository repo, IConfiguration config)
    { _repo = repo; _config = config; }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var usuario = await _repo.GetByEmailAsync(request.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
        };
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds,
            claims: claims);

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            usuario.Nome, usuario.Email, usuario.Perfil.ToString());
    }
}