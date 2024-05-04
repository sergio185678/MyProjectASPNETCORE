using Azure;
using MyProjectAPINETCore.Models;
using MyProjectAPINETCore.Models.Dto;
using X.PagedList;

namespace MyProjectAPINETCore.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> registrar_usuario(RegisterDto registerDto);
        Task<List<UsuarioDto>> obtener_usuarios();
        Task<UsuarioDto> obtener_usuario(int id);
        Task<UsuarioDto> editar_usuario(UsuarioEditarDto usuarioEditarDto, int id);
        Task eliminar_usuario(int id);
        Task<Usuario> obtener_usuario_mediante_credenciales(LoginDto loginDto);
        Task<Usuario> usuario_existent(string correo);
        Task<PaginaDto> obtenerusuariopaginado(int page, int size, string searchTerm);
    }
}
