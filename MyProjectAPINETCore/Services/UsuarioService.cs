using Azure;
using Microsoft.AspNetCore.Mvc;
using MyProjectAPINETCore.Models;
using MyProjectAPINETCore.Models.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using MyProjectAPINETCore.Utils;

namespace MyProjectAPINETCore.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DatabaseExampleContext _databaseExampleContext;
        public UsuarioService(DatabaseExampleContext databaseExampleContext)
        {
            _databaseExampleContext = databaseExampleContext;
        }

        public async Task<UsuarioDto> editar_usuario(UsuarioEditarDto usuarioEditarDto, int id)
        {
            Usuario usuario = _databaseExampleContext.Usuarios.Find(id);
            if (usuario == null) {
                return null;
            }
            Cargo cargo = _databaseExampleContext.Cargos.Find(usuarioEditarDto.IdCargo);
            if (cargo == null)
            {
                return null;
            }
            usuario.NombreCompleto=usuarioEditarDto.NombreCompleto;
            usuario.IdCargo= usuarioEditarDto.IdCargo;
            try
            {
                _databaseExampleContext.Usuarios.Update(usuario);
                await _databaseExampleContext.SaveChangesAsync();
                return convertirAUsuarioDTO(usuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task eliminar_usuario(int id)
        {
            Usuario usuario = await _databaseExampleContext.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _databaseExampleContext.Usuarios.Remove(usuario);
                _databaseExampleContext.SaveChanges();
            }
        }

        public async Task<PaginaDto> obtenerusuariopaginado(int page, int size, string searchTerm)
        {
            var listausuarios = _databaseExampleContext.Usuarios.Where(u=>u.NombreCompleto.Contains(searchTerm)).Include(u => u.oCargo).ToList();
            var totalElementos = listausuarios.Count();
            var pagedData = listausuarios.ToPagedList(page, size);
            var numberOfElements = pagedData.Count();
            var totalPages = (int)Math.Ceiling((double)totalElementos / size);
            bool esUltimaPagina = page == totalPages;

            PaginaDto paginaDto=new PaginaDto();
            paginaDto.content = pagedData.Select(convertirAUsuarioDTO);
            paginaDto.totalElementos = totalElementos;
            paginaDto.totalPages = totalPages;
            paginaDto.numberOfElements = numberOfElements;
            paginaDto.last = esUltimaPagina;

            return paginaDto;
        }

        public async Task<UsuarioDto> obtener_usuario(int id)
        {
            Usuario usuario = await _databaseExampleContext.Usuarios.Where(u=>u.UserId==id).Include(u => u.oCargo).FirstAsync();
            if (usuario == null)
            {
                return null;
            }
            return convertirAUsuarioDTO(usuario);
        }

        public async Task<List<UsuarioDto>> obtener_usuarios()
        {
            List<Usuario> listusuarios = await _databaseExampleContext.Usuarios.Include(u => u.oCargo).ToListAsync();
            return listusuarios.Select(convertirAUsuarioDTO).ToList();
        }

        public async Task<Usuario> obtener_usuario_mediante_credenciales(LoginDto loginDto)
        {
            Usuario usuarioexistente = await usuario_existent(loginDto.Correo);
            if (usuarioexistente == null)
            {
                return null;
            }
            if(usuarioexistente.Contraseña == Encriptar.EncriptarPasword(loginDto.Contraseña))
            {
                return usuarioexistente;
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuarioDto> registrar_usuario(RegisterDto registerDto)
        {
            Usuario usuario=new Usuario();
            usuario.NombreCompleto = registerDto.NombreCompleto;
            usuario.Correo = registerDto.Correo;
            usuario.Contraseña = Encriptar.EncriptarPasword(registerDto.Contraseña);
            usuario.IdCargo=registerDto.IdCargo;
            Cargo cargo = await _databaseExampleContext.Cargos.FindAsync(registerDto.IdCargo);
            usuario.oCargo = cargo;
            try
            {
                _databaseExampleContext.Usuarios.Add(usuario);
                await _databaseExampleContext.SaveChangesAsync();
                return convertirAUsuarioDTO(usuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> usuario_existent(string correo)
        {
            return await _databaseExampleContext.Usuarios.Where(u=>u.Correo.Equals(correo)).Include(u=>u.oCargo).FirstOrDefaultAsync();
        }

        private UsuarioDto convertirAUsuarioDTO(Usuario usuario)
        {
            UsuarioDto usuarioDto = new UsuarioDto();
            usuarioDto.UserId = usuario.UserId;
            usuarioDto.NombreCompleto = usuario.NombreCompleto;
            usuarioDto.Correo = usuario.Correo;
            usuarioDto.Cargo = usuario.oCargo;
            return usuarioDto;
        }
    }
}
