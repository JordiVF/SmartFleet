using System;
using System.Collections.Generic;

namespace SmartFleet.Model;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Username { get; set; } = null!;

    public string Passw { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public byte[]? FotoPerfil { get; set; }
}
