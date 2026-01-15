using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFleet.Model
{
    public static class EmpleadoLogueado
    {
        public static int IdEmpleado { get; set; }

        public static string Username { get; set; } = null!;

        public static string Passw { get; set; } = null!;

        public static string NombreCompleto { get; set; } = null!;

        public static byte[]? FotoPerfil { get; set; }
    }
}
