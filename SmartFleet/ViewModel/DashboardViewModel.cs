using LiveCharts;
using SmartFleet.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartFleet.ViewModel
{
    public class DashboardViewModel
    {
        public static List<VehiculoAlquilerData> ObtenerDatosVehiculosAlquiler()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                var query = from vehiculo in model.Vehiculos
                            join alquiler in model.Alquilers on vehiculo.IdVehiculo equals alquiler.IdVehiculo into alquileres
                            select new VehiculoAlquilerData
                            {
                                Marca = vehiculo.Marca,
                                Modelo = vehiculo.Modelo,
                                CantidadAlquileres = alquileres.Count()
                            };

                return query.ToList();
            }
        }
        
        public static ChartValues<double> GetCosteMantenimientoMensual()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                var query = from mantenimiento in model.Mantenimientos
                            where mantenimiento.Fecha.HasValue && mantenimiento.Fecha.Value.Year == 2023
                            group mantenimiento by mantenimiento.Fecha.Value.Month into g
                            select new
                            {
                                Mes = g.Key,
                                CosteMensual = g.Sum(m => m.Coste)
                            };

                List<double> costesMensuales = query.OrderBy(m => m.Mes)
                                                    .Select(m => m.CosteMensual)
                                                    .ToList();

                return new ChartValues<double>(costesMensuales);
            }
        }
    }
    public class VehiculoAlquilerData
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int CantidadAlquileres { get; set; }
    }
}
