using Microsoft.EntityFrameworkCore;
using SmartFleet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmartFleet.ViewModel
{
    public class EmpleadoViewModel
    {
        public String GetSHA256(string clave)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();

            StringBuilder sb = new StringBuilder();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(clave));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public Empleado ValidarInicioSesion(Empleado empleado)
        {

            Empleado empleadoLogeado;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                empleadoLogeado = model.Empleados.FirstOrDefault(e => e.Username == empleado.Username && e.Passw == GetSHA256(empleado.Passw));
                if (empleadoLogeado != null)
                {
                    return empleadoLogeado;
                }
            }
            return null;
        }
        public List<Empleado> SelectAll()
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                List<Empleado> list = model.Empleados.OrderBy(emp => emp.IdEmpleado).ToList();
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
        public bool InsertarEmpleado(Empleado e)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Empleados.Add(e);
                if (model.SaveChanges() == 1) {
                    ok = true;
                }
            }
            return ok;
        }
        public bool ModificarEmpleado(Empleado empleado)
        {
            bool ok = false;
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                model.Empleados.Update(empleado);
                if (model.SaveChanges() == 1)
                    ok = true;
            }
            return ok;
        }
        public bool BorrarEmpleado(Empleado empleado)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                try
                {
                    model.Empleados.Remove(empleado);
                    model.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
        public List<Empleado> SelectBuscarEmpleados(string busqueda)
        {
            using (SmartFleetDbContext model = new SmartFleetDbContext())
            {
                return model.Empleados.Where(e => e.Username.Contains(busqueda) || e.Nombre.Contains(busqueda) || e.Apellidos.Contains(busqueda)).ToList();
            }
            return null;
        }
    }
}
