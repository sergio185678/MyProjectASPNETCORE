using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyProjectAPINETCore.Models;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string Nombre { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
