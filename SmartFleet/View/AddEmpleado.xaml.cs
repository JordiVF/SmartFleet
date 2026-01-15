using Microsoft.Win32;
using SmartFleet.Model;
using SmartFleet.ViewModel;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para AddEmpleado.xaml
    /// </summary>
    public partial class AddEmpleado : Window
    {
        byte[] foto;
        private EmpleadoViewModel empleadoViewModel;
        private Empleado empleadomodificar;
        private bool modificar = false;
        public AddEmpleado()
        {
            InitializeComponent();
            modificar = false;
            this.empleadoViewModel = new EmpleadoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public AddEmpleado(Empleado empleadoModificar)
        {
            InitializeComponent();
            this.empleadoViewModel = new EmpleadoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            if (empleadoModificar != null)
            {
                this.empleadomodificar = empleadoModificar;
                txtUser.Text = empleadoModificar.Username;
                txtNombre.Text = empleadoModificar.Nombre;
                txtApellidos.Text = empleadoModificar.Apellidos;
                txtDireccion.Text = empleadoModificar.Direccion;
                txtTelefono.Text = empleadoModificar.Telefono;
                txtEmail.Text = empleadoModificar.Email;
                dtFechaContratacion.SelectedDate = empleadoModificar.FechaContratacion;
                dtFechaNacimiento.SelectedDate = empleadoModificar.FechaNacimiento;
                if (empleadoModificar.FotoPerfil != null)
                {
                    MemoryStream ms = new MemoryStream(empleadoModificar.FotoPerfil);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgFotoPerfil.Source = bitmap;
                }
                modificar = true;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = EmpleadoLogueado.Username.ToString();
            if (EmpleadoLogueado.FotoPerfil != null)
            {
                MemoryStream ms = new MemoryStream(EmpleadoLogueado.FotoPerfil);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                imgUser.ImageSource = bitmap;
            }
        }
        private void btn_Empleados_Click(object sender, RoutedEventArgs e)
        {
            EmpleadosView empleadosView = new EmpleadosView();
            empleadosView.Show();
            this.Close();
        }

        private void btn_Clientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesView clienteView = new ClientesView();
            clienteView.Show();
            this.Close();
        }

        private void btn_Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            VehiculosView vehiculosView = new VehiculosView();
            vehiculosView.Show();
            this.Close();
        }

        private void btn_Mantenimiento_Click(object sender, RoutedEventArgs e)
        {
            MantenimientosView mantenimientosView = new MantenimientosView();
            mantenimientosView.Show();
            this.Close();
        }

        private void btn_Alquiler_Click(object sender, RoutedEventArgs e)
        {
            AlquilerView alquilerView = new AlquilerView();
            alquilerView.Show();
            this.Close();
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres desconectarte?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Login login = new Login();
                login.Show();
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                this.Close();
            }
        }
        /// <summary>
        /// Permite arrastrar una ventana sin usar su barra de título.
        /// Se hace uso de la función "SendMessage" de la biblioteca "user32.dll"
        /// para enviar un mensaje a la ventana especificada por "helper.Handle"
        /// con un identificador de mensaje de 161, y parámetros de wParam y lParam como 2 y 0, respectivamente.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

        }
        /// <summary>
        /// Funcion que cierra la Aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            }
        }
        /// <summary>
        /// Funcion que maximiza la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        /// <summary>
        /// Funcion que minimiza la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnEliminarFoto_Click(object sender, RoutedEventArgs e)
        {
            imgFotoPerfil.Source = null;
        }
        private void btnSelectFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.png)|*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Seleccionar imagen";
            openFileDialog.RestoreDirectory = true;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                foto = File.ReadAllBytes(openFileDialog.FileName);
                imgFotoPerfil.Source = empleadoViewModel.AddImagen(foto);
            }
        }

        private void btnAddEmpleado_Click(object sender, RoutedEventArgs e)
        {
            bool validacion = true;
            Empleado empleado = new Empleado();
            string patternTelefono = @"^\+(?:[0-9] ?){6,14}[0-9]$";
            string patternEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Match matchEmail = Regex.Match(txtEmail.Text, patternEmail);
            Match matchTelefono = Regex.Match(txtTelefono.Text, patternTelefono);
            byte[] fotoPerfil = null;

            if (txtUser == null || txtUser.Text == "" || txtPassw.SecurePassword.Length == 0 || txtPassw == null || txtNombre == null
                || txtNombre.Text == "" || txtApellidos.Text == "" || txtApellidos == null || txtDireccion.Text == "" || txtDireccion == null)
            {
                MessageBox.Show("Todos los campos son obligatorios, menos la foto.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (dtFechaNacimiento.SelectedDate >= DateTime.Now)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser mayor a la fecha de hoy o igual", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (txtTelefono.Text.Length > 0)
            {
                if (matchTelefono.Success && matchTelefono.Value.Length == txtTelefono.Text.Length) { }
                else
                {
                    MessageBox.Show("El numero de telefono es invalido, prueba con el formato: +XX XXXXXXXXX", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    validacion = false;
                }
            }
            if (txtEmail.Text.Length > 0)
            {
                if (matchEmail.Success && matchEmail.Value.Length == txtEmail.Text.Length){}
                else
                {
                    MessageBox.Show("El correo electronico es invalido", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    validacion = false;
                }
            }
            if (imgFotoPerfil.Source != null)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgFotoPerfil.Source));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    fotoPerfil = ms.ToArray();
                }
            }
            if (imgFotoPerfil.Source == null)
            {
                fotoPerfil = null;
            }
            if (validacion)
            {
                if (modificar)
                {
                    empleado.IdEmpleado = empleadomodificar.IdEmpleado;
                    empleado.Username = txtUser.Text;
                    empleado.Passw = empleadoViewModel.GetSHA256(txtPassw.Password);
                    empleado.Nombre = txtNombre.Text;
                    empleado.Apellidos = txtApellidos.Text;
                    empleado.Direccion = txtDireccion.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Email = txtEmail.Text;
                    empleado.FechaNacimiento = dtFechaNacimiento.SelectedDate;
                    empleado.FechaContratacion = dtFechaContratacion.SelectedDate;
                    empleado.FotoPerfil = fotoPerfil;
                    if (empleadoViewModel.ModificarEmpleado(empleado))
                    {
                        MessageBox.Show("Empleado modificado correctamente");
                        LimpiarCampos();
                        empleado = null;
                    }
                }
                if (!modificar)
                {
                    empleado.Username = txtUser.Text;
                    empleado.Passw = empleadoViewModel.GetSHA256(txtPassw.Password);
                    empleado.Nombre = txtNombre.Text;
                    empleado.Apellidos = txtApellidos.Text;
                    empleado.Direccion = txtDireccion.Text;
                    empleado.Telefono = txtTelefono.Text;
                    empleado.Email = txtEmail.Text;
                    empleado.FechaNacimiento = dtFechaNacimiento.SelectedDate;
                    empleado.FechaContratacion = dtFechaContratacion.SelectedDate;
                    empleado.FotoPerfil = fotoPerfil;
                    if (empleadoViewModel.InsertarEmpleado(empleado))
                    {
                        MessageBox.Show("Empleado insertado correctamente.");
                        LimpiarCampos();
                        empleado = null;
                    }
                }
            }
        }
        private void LimpiarCampos()
        {
            txtUser.Clear();
            txtPassw.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            dtFechaContratacion.SelectedDate = null;
            dtFechaNacimiento.SelectedDate = null;
            imgFotoPerfil.Source = null;
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard  dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}
