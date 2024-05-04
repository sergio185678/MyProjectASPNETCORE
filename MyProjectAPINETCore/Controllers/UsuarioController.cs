using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProjectAPINETCore.Models;
using MyProjectAPINETCore.Models.Dto;
using MyProjectAPINETCore.Services;
using MyProjectAPINETCore.Utils;
using System.Security.Claims;

namespace MyProjectAPINETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPut]
        [Route("usuario/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> editar_usuario([FromBody] UsuarioEditarDto usuarioEditarDto, int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (JwtConfigurator.GetTokenCargo(identity) != "Administrador")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "No estas autorizado." });
            }

            var usuario = await _usuarioService.editar_usuario(usuarioEditarDto, id);
            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Hubo algun error al actualizar" });
                
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se actualizo correctamente",response= usuario });
        }

        [HttpDelete]
        [Route("usuario/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> eliminar_usuario(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (JwtConfigurator.GetTokenCargo(identity) != "Administrador")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "No estas autorizado." });
            }

            try
            {
                await _usuarioService.eliminar_usuario(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se elimino correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }
            
        }

        [HttpGet]
        [Route("usuariospaginacion")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtenerUsuariosPaginados(int page = 1, int size = 10, string searchTerm = "")
        {
            var pagedData = await _usuarioService.obtenerusuariopaginado(page, size, searchTerm);
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "", response = pagedData });
        }

        [HttpGet]
        [Route("usuario/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtener_usuario(int id)
        {
            UsuarioDto usuario = await _usuarioService.obtener_usuario(id);
            if(usuario == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontro el usuario." });
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "", response = usuario });
        }

        [HttpGet]
        [Route("usuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtener_usuarios()
        {
            List<UsuarioDto> usuarios = await _usuarioService.obtener_usuarios();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "lista de usuarios", response = usuarios });
        }

        [HttpGet]
        [Route("user_by_jwt")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtenerusuarioporjwt()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return await obtener_usuario(JwtConfigurator.GetTokenIdUsuario(identity));
        }
    }
}
