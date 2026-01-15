using Microsoft.EntityFrameworkCore;
using SmartFleet.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SmartFleet.ViewModel
{
    public class MantenimientoViewModel
    {
        public List<Mantenimiento> SelectAll()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Mantenimiento> list = model.Mantenimientos
                    .Include(a => a.IdVehiculoNavigation) 
                    .OrderBy(emp => emp.IdMantenimiento)
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
        public bool InsertarMantenimiento(Mantenimiento e)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Mantenimientos.Add(e);
                if (model.SaveChanges() == 1)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public bool ModificarMantenimiento(Mantenimiento mantenimiento)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Mantenimientos.Update(mantenimiento);
                if (model.SaveChanges() == 1)
                    ok = true;
            }
            return ok;
        }
        public bool BorrarMantenimiento(Mantenimiento mantenimiento)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                try
                {
                    model.Mantenimientos.Remove(mantenimiento);
                    model.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public List<Mantenimiento> SelectBuscarMantenimiento(string busqueda)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Mantenimiento> mantenimientosEncontrados = model.Mantenimientos
                    .Include(a => a.IdVehiculoNavigation)
                    .Where(a => a.Descripcion.Contains(busqueda)
                    || a.IdVehiculoNavigation.Matricula.Contains(busqueda)
                    || a.IdVehiculoNavigation.Marca.Contains(busqueda)
                    || a.IdVehiculoNavigation.Modelo.Contains(busqueda))
                    .ToList();
                return mantenimientosEncontrados;
            }
        }
    }
}
