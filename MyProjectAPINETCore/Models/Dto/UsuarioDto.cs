namespace MyProjectAPINETCore.Models.Dto
{
    public class UsuarioDto
    {
        public int UserId { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; } = null!;
        public virtual Cargo? Cargo { get; set; }
    }
}
