namespace MyProjectAPINETCore.Models.Dto
{
    public class RegisterDto
    {
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int? IdCargo { get; set; }
    }
}
