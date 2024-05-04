using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using MyProjectAPINETCore.Models;
using System;

namespace MyProjectAPINETCore.Services
{
    public class DocumentoService : IDocumentoService
    {

        private readonly DatabaseExampleContext _exampleContext;
        private readonly string _uploadsDirectory;
        public DocumentoService(DatabaseExampleContext exampleContext)
        {
            _exampleContext = exampleContext;
            _uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(_uploadsDirectory))
            {
                Directory.CreateDirectory(_uploadsDirectory);
            }
        }

        public async Task eliminar_documento(int id)
        {
            Documento documento = await _exampleContext.Documentos.FindAsync(id);
            if (documento != null)
            {
                string[] partes = documento.Ruta.Split('/');

                // El último elemento en el array partes será el nombre del archivo
                string fileName = partes[partes.Length - 1];

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
                if (File.Exists(filePath))
                {
                    // Elimina el archivo
                    File.Delete(filePath);
                }

                _exampleContext.Documentos.Remove(documento);
                _exampleContext.SaveChanges();
            }
        }

        public async Task<Documento> obtenerDocumento(int documetoid)
        {
            return await _exampleContext.Documentos.Where(d => d.IdDocumento == documetoid).FirstOrDefaultAsync();
        }

        public async Task<List<Documento>> obtenerListaDocumentosUsuario(int userid)
        {
            return await _exampleContext.Documentos.Where(d => d.IdUsuario == userid).ToListAsync();
        }

        public async Task registrarDocumento(IFormFile file, string tipo, int usuarioid, string uniqueFileName, string ruta)
        {
            // Construct the path where you want to save the file
            string filePath = Path.Combine(_uploadsDirectory, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Documento documento = new Documento();
            documento.Ruta = ruta;
            documento.TipoDocumento = tipo;
            documento.IdUsuario = usuarioid;
            _exampleContext.Documentos.Add(documento);
            await _exampleContext.SaveChangesAsync();
        }
    }
}
