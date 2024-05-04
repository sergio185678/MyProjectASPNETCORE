using X.PagedList;

namespace MyProjectAPINETCore.Models.Dto
{
    public class PaginaDto
    {
        public IPagedList<UsuarioDto> content { get; set; }
        public int totalElementos { get; set; }
        public int totalPages { get; set; }
        public int numberOfElements { get; set; }//elementos actuales
        public bool last { get; set; }//ultima pagina
    }
}
