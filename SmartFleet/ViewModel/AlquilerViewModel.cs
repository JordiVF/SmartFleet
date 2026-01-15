using Microsoft.EntityFrameworkCore;
using SmartFleet.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SmartFleet.ViewModel
{
    public class AlquilerViewModel
    {
        public List<Alquiler> SelectAll()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Alquiler> list = model.Alquilers
                    .Include(a => a.IdClienteNavigation)
                    .Include(a => a.IdVehiculoNavigation)
                    .OrderBy(emp => emp.IdAlquiler)
                    .ToList();
                return list;
            }
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
        public bool InsertarAlquiler(Alquiler e)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Alquilers.Add(e);
                if (model.SaveChanges() == 1)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public bool ModificarAlquiler(Alquiler alquiler)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Alquilers.Update(alquiler);
                if (model.SaveChanges() == 1)
                    ok = true;
            }
            return ok;
        }
        public bool BorrarAlquiler(Alquiler alquiler)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                try
                {
                    model.Alquilers.Remove(alquiler);
                    model.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public List<Alquiler> SelectBuscarAlquileres(string busqueda)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Alquiler> alquileresEncontrados = model.Alquilers
                    .Include(a => a.IdClienteNavigation)
                    .Include(a => a.IdVehiculoNavigation)
                    .Where(a => a.IdClienteNavigation.Nombre.Contains(busqueda)
                    || a.IdClienteNavigation.Apellidos.Contains(busqueda)
                    || a.IdVehiculoNavigation.Marca == busqueda
                    || a.IdVehiculoNavigation.Modelo == busqueda
                    || a.IdVehiculoNavigation.Matricula.Contains(busqueda))
                    .ToList();
                return alquileresEncontrados;
            }
        }
    }
}
