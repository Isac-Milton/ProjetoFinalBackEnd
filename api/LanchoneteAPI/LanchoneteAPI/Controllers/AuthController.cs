using Microsoft.AspNetCore.Mvc;
using LanchoneteAPI.DTOs;
using LanchoneteAPI.Services;

namespace LanchoneteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    public AuthController(IAuthService service) => _service = service;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _service.LoginAsync(request);
        return result is null
            ? Unauthorized(new { mensagem = "Email ou senha inválidos." })
            : Ok(result);
    }
}