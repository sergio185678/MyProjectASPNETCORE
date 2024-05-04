using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
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
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;
        public AuthController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> registrar_usuario([FromBody] RegisterDto registerDto)
        {
            Usuario usuarioexistente = await _usuarioService.usuario_existent(registerDto.Correo);
            if (usuarioexistente!=null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new { mensaje = "El usuario ya existe. Por favor, elige otro correo electrónico." });
            }

            UsuarioDto usuarioDto=await _usuarioService.registrar_usuario(registerDto);
            if(usuarioDto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al registrar." });
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se registro correctamente.",response=usuarioDto });
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login([FromBody] LoginDto loginDto)
        {
            Usuario usuariologeado = await _usuarioService.obtener_usuario_mediante_credenciales(loginDto);
            if(usuariologeado == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "Correo o contraseña incorrecta" });
            }
            else
            {
                string tokenString = JwtConfigurator.GetToken(usuariologeado, this._configuration);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se logueo correctamente", response = tokenString });
            }
        }

        [HttpGet]
        [Route("validar_token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<bool> validar_token()
        {
            return true;
        }

        [HttpGet]
        [Route("info_user_jwt")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> get_info_jwt()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return StatusCode(StatusCodes.Status200OK, 
                new { mensaje = "",
                    usuario = JwtConfigurator.GetTokenSubject(identity),
                    cargo = JwtConfigurator.GetTokenCargo(identity) });
        }
    }
}
