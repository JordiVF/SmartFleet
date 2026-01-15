using SmartFleet.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartFleet.ViewModel
{
    public class ClienteViewModel
    {
        public List<Cliente> SelectAll()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Cliente> list = model.Clientes.OrderBy(emp => emp.IdCliente).ToList();
                return list;
            }
        }
        public List<Cliente> SelectBuscarClientes(string busqueda)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                return model.Clientes.Where(e => e.Nombre.Contains(busqueda) || e.Apellidos.Contains(busqueda)).ToList();
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
        public Cliente SelectById(int idCliente)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                Cliente cliente = model.Clientes.FirstOrDefault(emp => emp.IdCliente == idCliente);
                return cliente;
            }
        }
        public bool InsertarCliente(Cliente e)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Clientes.Add(e);
                if (model.SaveChanges() == 1)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public bool ModificarCliente(Cliente cliente)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Clientes.Update(cliente);
                if (model.SaveChanges() == 1)
                    ok = true;
            }
            return ok;
        }
        public void DeleteCliente(int idCliente)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                bool isRelatedToAlquiler = model.Alquilers.Any(a => a.IdCliente == idCliente);
                if (isRelatedToAlquiler)
                {
                    var result = MessageBox.Show("El cliente está relacionado con al menos un alquiler. ¿Deseas continuar con la eliminación?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("El cliente no se ha borrado");
                        return;
                    }
                }

                var alquileresRelacionados = model.Alquilers.Where(a => a.IdCliente == idCliente);
                model.Alquilers.RemoveRange(alquileresRelacionados);

                Cliente cliente = model.Clientes.Find(idCliente);
                if (cliente != null)
                {
                    model.Clientes.Remove(cliente);
                    model.SaveChanges();
                    MessageBox.Show("Cliente borrado correctamente");
                }
            }
        }
    }
}
