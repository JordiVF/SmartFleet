using System;
using System.Collections.Generic;

namespace SmartFleet.Model;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public string Matricula { get; set; } = null!;

    public int Kilometraje { get; set; }

    public bool Disponible { get; set; }

    public double Precio { get; set; }

    public byte[]? FotoVehiculo { get; set; }

    public virtual ICollection<Alquiler> Alquilers { get; set; } = new List<Alquiler>();

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
