namespace LanchoneteAPI.DTOs;

public record LoginRequest(string Email, string Senha);
public record LoginResponse(string Token, string Nome, string Email, string Perfil);