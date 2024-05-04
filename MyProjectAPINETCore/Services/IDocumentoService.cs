using Microsoft.AspNetCore.Mvc;
using MyProjectAPINETCore.Models;

namespace MyProjectAPINETCore.Services
{
    public interface IDocumentoService
    {
        Task registrarDocumento(IFormFile file, string tipo, int usuarioid, string uniqueFileName, string ruta);
        Task<List<Documento>> obtenerListaDocumentosUsuario(int userid);
        Task eliminar_documento(int id);
        Task<Documento> obtenerDocumento(int documetoid);
    }
}
