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
    /// Lógica de interacción para AddVehiculo.xaml
    /// </summary>
    public partial class AddVehiculo : Window
    {
        byte[] foto;
        private VehiculoViewModel vehiculoViewModel;
        private Vehiculo vehiculomodificar;
        private bool modificar = false;
        public AddVehiculo()
        {
            InitializeComponent();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public AddVehiculo(Vehiculo vehiculoModificar)
        {
            InitializeComponent();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            if (vehiculoModificar != null)
            {
                this.vehiculomodificar = vehiculoModificar;
                txtMarca.Text = vehiculoModificar.Marca;
                txtModelo.Text = vehiculoModificar.Modelo;
                txtMatricula.Text = vehiculoModificar.Matricula;
                txtKilometraje.Text = vehiculoModificar.Kilometraje.ToString();
                if (vehiculoModificar.Disponible)
                {
                    rbDisponible.IsChecked = true;
                }
                if (!vehiculoModificar.Disponible)
                {
                    rbNoDisponible.IsChecked = true;
                }
                txtPrecio.Text = vehiculoModificar.Precio.ToString();
                if (vehiculoModificar.FotoVehiculo != null)
                {
                    MemoryStream ms = new MemoryStream(vehiculoModificar.FotoVehiculo);

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgFotoVehiculo.Source = bitmap;
                }
                modificar = true;
            }
        }

        private void btnAddVehiculo_Click(object sender, RoutedEventArgs e)
        {
            bool validacion = true;
            bool disponibilidad = false;
            Vehiculo vehiculo = new Vehiculo();
            Regex regexPrecio = new Regex("[^0-9.,-]+");
            Regex regexKm = new Regex(@"^[0-9]+$");
            byte[] fotoVehiculo = null;

            if (txtMarca == null || txtMarca.Text == "" || txtModelo.Text == "" || txtModelo == null || txtMatricula == null || txtMatricula.Text == "" ||
                txtKilometraje.Text == "" || txtKilometraje == null || txtPrecio.Text == "" || txtPrecio == null)
            {
                MessageBox.Show("Todos los campos son obligatorios, menos la foto.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (!rbDisponible.IsChecked.HasValue || !rbNoDisponible.IsChecked.HasValue || (!rbDisponible.IsChecked.Value && !rbNoDisponible.IsChecked.Value))
            {
                MessageBox.Show("Debes seleccionar si el vehiculo está disponible o no");
                validacion = false;
            }
            if (regexPrecio.IsMatch(txtPrecio.Text))
            {
                MessageBox.Show("Debes introducir un numero para el precio, para los decimales puedes usar , o .");
                validacion = false;
            }
            if (!regexKm.IsMatch(txtKilometraje.Text))
            {
                MessageBox.Show("Debes introducir un numero para el kilometraje");
                validacion = false;
            }
            if (rbDisponible.IsChecked == true)
            {
                disponibilidad = true;
            }
            if (rbNoDisponible.IsChecked == true)
            {
                disponibilidad = false;
            }
            if (imgFotoVehiculo.Source != null)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgFotoVehiculo.Source));

                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    fotoVehiculo = ms.ToArray();
                }
            }
            if (imgFotoVehiculo.Source == null)
            {
                fotoVehiculo = null;
            }
            if (validacion)
            {
                if (modificar)
                {
                    vehiculo.IdVehiculo = vehiculomodificar.IdVehiculo;
                    vehiculo.Marca = txtMarca.Text;
                    vehiculo.Modelo = txtModelo.Text;
                    vehiculo.Matricula = txtMatricula.Text;
                    vehiculo.Kilometraje = Convert.ToInt32(txtKilometraje.Text);
                    vehiculo.Disponible = disponibilidad;
                    vehiculo.Precio = Convert.ToInt32(txtPrecio.Text);
                    vehiculo.FotoVehiculo = fotoVehiculo;
                    if (vehiculoViewModel.ModificarVehiculo(vehiculo))
                    {
                        MessageBox.Show("Vehiculo modificado correctamente");
                        LimpiarCampos();
                        vehiculo = null;
                    }
                }
                if (!modificar)
                {
                    vehiculo.Marca = txtMarca.Text;
                    vehiculo.Modelo = txtModelo.Text;
                    vehiculo.Matricula = txtMatricula.Text;
                    vehiculo.Kilometraje = Convert.ToInt32(txtKilometraje.Text);
                    vehiculo.Disponible = disponibilidad;
                    vehiculo.Precio = Convert.ToInt32(txtPrecio.Text);
                    vehiculo.FotoVehiculo = fotoVehiculo;
                    if (vehiculoViewModel.InsertarVehiculo(vehiculo))
                    {
                        MessageBox.Show("Vehiculo insertado correctamente.");
                        LimpiarCampos();
                        vehiculo = null;
                    }
                }
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
            imgFotoVehiculo.Source = null;
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
                imgFotoVehiculo.Source = vehiculoViewModel.AddImagen(foto);
            }
        }
        private void LimpiarCampos()
        {
            txtKilometraje.Clear();
            txtMarca.Clear();
            txtMatricula.Clear();
            txtModelo.Clear();
            txtPrecio.Clear();
            imgFotoVehiculo.Source = null;
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}
