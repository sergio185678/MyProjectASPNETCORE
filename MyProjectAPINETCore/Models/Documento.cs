using System;
using System.Collections.Generic;

namespace MyProjectAPINETCore.Models;

public partial class Documento
{
    public int IdDocumento { get; set; }

    public string? TipoDocumento { get; set; }

    public string? Ruta { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? oUsuario { get; set; }
}
