using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyProjectAPINETCore.Models;

public partial class Usuario
{
    public int UserId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public int? IdCargo { get; set; }
    [JsonIgnore]
    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual Cargo? oCargo { get; set; }
}
