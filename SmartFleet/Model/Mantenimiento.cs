using System;
using System.Collections.Generic;

namespace SmartFleet.Model;

public partial class Mantenimiento
{
    public int IdMantenimiento { get; set; }

    public DateTime? Fecha { get; set; }

    public string Descripcion { get; set; } = null!;

    public double Coste { get; set; }

    public int IdVehiculo { get; set; }

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
