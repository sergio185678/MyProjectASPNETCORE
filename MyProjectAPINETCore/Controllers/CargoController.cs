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
    public class CargoController : ControllerBase
    {
        private readonly ICargoService cargoService;

        public CargoController(ICargoService cargoService)
        {
            this.cargoService = cargoService;
        }

        [HttpPost]
        [Route("register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> registrar_cargo([FromBody] Cargo cargo)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(JwtConfigurator.GetTokenCargo(identity)!= "Administrador")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "No estas autorizado." });
            }

            if (await cargoService.registrarCargo(cargo) == true)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se registro correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al registrar." });
            }
        }

        [HttpGet]
        [Route("cargos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtenercargos()
        {
            List<Cargo> cargos = await cargoService.obtenerCargos();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "lista de cargos", response = cargos });
        }

        [HttpGet]
        [Route("usuarios_by_cargo/{cargo_id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtenerUsuariosPorCargo(int cargo_id)
        {
            List<Usuario> usuarios = await cargoService.obtenerUsuariosPorCargo(cargo_id);
            if(usuarios.Count == 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "No hay usuarios." });
            }
            
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "lista de usarios", response = usuarios.Select(convertirAUsuarioDTO).ToList() });
        }

        [HttpGet]
        [Route("cargo/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtener_cargo(int id)
        {
            Cargo cargo = await cargoService.obtenerCargo(id);
            if (cargo==null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontro el cargo." });
            }
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "", response = cargo });
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
