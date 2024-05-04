using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MyProjectAPINETCore.Models;
using MyProjectAPINETCore.Services;
using MyProjectAPINETCore.Utils;
using System.Security.Claims;

namespace MyProjectAPINETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;
        public DocumentController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }

        [HttpPost]
        [Route("save_files")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> guardar_archivos(IFormFile file,string tipo,int usuarioid)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (JwtConfigurator.GetTokenCargo(identity) != "Administrador")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "No estas autorizado." });
            }

            if (file == null || file.Length == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al enviar archivo." });
            }

            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string fileUrl = Url.Content("~/Uploads/" + uniqueFileName);
                await _documentoService.registrarDocumento(file, tipo, usuarioid, uniqueFileName, fileUrl);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se registro el archivo correctamente", response = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al registrar." });
            }
        }

        [HttpGet]
        [Route("get_all_files_by_user/{user_id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtener_archivos(int user_id)
        {
            List<Documento> documentos = await _documentoService.obtenerListaDocumentosUsuario(user_id);
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Lista de documentos.", response = documentos });
        }

        [HttpGet]
        [Route("get_url/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> obtener_ruta(int id)
        {
            Documento documento = await _documentoService.obtenerDocumento(id);
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ruta del archivo.", response = documento.Ruta });
        }

        [HttpDelete]
        [Route("file/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> eliminar_Archivo(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (JwtConfigurator.GetTokenCargo(identity) != "Administrador")
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { mensaje = "No estas autorizado." });
            }

            try
            {
                await _documentoService.eliminar_documento(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se elimino el archivo correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = "Error al registrar." });
            }
        }
    }
}
