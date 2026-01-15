using SmartFleet.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartFleet.ViewModel
{
    public class VehiculoViewModel
    {
        public List<Vehiculo> SelectAll()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Vehiculo> list = model.Vehiculos.OrderBy(emp => emp.IdVehiculo).ToList();
                return list;
            }
        }
        public List<Vehiculo> SelectBuscarVehiculo(string busqueda)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                return model.Vehiculos.Where(e => e.Matricula.Contains(busqueda) || e.Modelo.Contains(busqueda) || e.Marca.Contains(busqueda)).ToList();
            }
            return null;
        }
        public BitmapImage AddImagen(byte[] imagen)
        {
            if (imagen == null || imagen.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imagen))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public bool InsertarVehiculo(Vehiculo e)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Vehiculos.Add(e);
                if (model.SaveChanges() == 1)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public bool ModificarVehiculo(Vehiculo vehiculo)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Vehiculos.Update(vehiculo);
                if (model.SaveChanges() == 1)
                    ok = true;
            }
            return ok;
        }
        public void DeleteVehiculo(int idVehiculo)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                bool isRelatedToAlquiler = model.Alquilers.Any(a => a.IdVehiculo == idVehiculo);
                if (isRelatedToAlquiler)
                {
                    var result = MessageBox.Show("El vehículo está relacionado con al menos un alquiler. ¿Deseas continuar con la eliminación?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("El vehiculo no se ha borrado");
                        return;
                    }
                }

                bool isRelatedToMantenimiento = model.Mantenimientos.Any(m => m.IdVehiculo == idVehiculo);
                if (isRelatedToMantenimiento)
                {
                    var result = MessageBox.Show("El vehículo está relacionado con al menos un mantenimiento. ¿Deseas continuar con la eliminación?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("El vehiculo no se ha borrado");
                        return;
                    }
                }

                var alquileresRelacionados = model.Alquilers.Where(a => a.IdVehiculo == idVehiculo);
                model.Alquilers.RemoveRange(alquileresRelacionados);

                var mantenimientosRelacionados = model.Mantenimientos.Where(m => m.IdVehiculo == idVehiculo);
                model.Mantenimientos.RemoveRange(mantenimientosRelacionados);

                Vehiculo vehiculo = model.Vehiculos.Find(idVehiculo);
                if (vehiculo != null)
                {
                    model.Vehiculos.Remove(vehiculo);
                    model.SaveChanges();
                    MessageBox.Show("Vehiculo borrado correctamente");
                }
            }
        }

    }
}
