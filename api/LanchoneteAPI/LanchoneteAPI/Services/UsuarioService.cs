using LanchoneteAPI.DTOs;
using LanchoneteAPI.Models;
using LanchoneteAPI.Repositories;

namespace LanchoneteAPI.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;
    public UsuarioService(IUsuarioRepository repo) => _repo = repo;

    private static UsuarioDto ToDto(Usuario u) =>
        new(u.Id, u.Nome, u.Email, u.Perfil.ToString(), u.Telefone, u.Endereco, u.Ativo, u.CriadoEm);

    public async Task<List<UsuarioDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    { var u = await _repo.GetByIdAsync(id); return u is null ? null : ToDto(u); }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioRequest r)
    {
        var perfil = Enum.Parse<PerfilUsuario>(r.Perfil, true);
        var u = await _repo.CreateAsync(new Usuario
        {
            Nome = r.Nome,
            Email = r.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(r.Senha),
            Perfil = perfil,
            Telefone = r.Telefone,
            Endereco = r.Endereco
        });
        return ToDto(u);
    }

    public async Task<UsuarioDto?> UpdateAsync(int id, UpdateUsuarioRequest r)
    {
        var u = await _repo.GetByIdAsync(id);
        if (u is null) return null;
        u.Nome = r.Nome; u.Telefone = r.Telefone; u.Endereco = r.Endereco; u.Ativo = r.Ativo;
        return ToDto(await _repo.UpdateAsync(u));
    }
}