using System;
using System.Collections.Generic;

namespace SmartFleet.Model;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Provincia { get; set; } = null!;

    public virtual ICollection<Alquiler> Alquilers { get; set; } = new List<Alquiler>();
}
