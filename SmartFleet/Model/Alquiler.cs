using System;
using System.Collections.Generic;

namespace SmartFleet.Model;

public partial class Alquiler
{
    public int IdAlquiler { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public double PrecioTotal { get; set; }

    public int IdCliente { get; set; }

    public int IdVehiculo { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
