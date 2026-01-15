using SmartFleet.Model;
using SmartFleet.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para AddMantenimientos.xaml
    /// </summary>
    public partial class AddMantenimientos : Window
    {
        private MantenimientoViewModel mantenimientoViewModel;
        private VehiculoViewModel vehiculoViewModel;
        private Vehiculo vehiculo;
        private Mantenimiento mantenimientomodificar;
        private bool modificar = false;
        public AddMantenimientos()
        {
            InitializeComponent();
            this.mantenimientoViewModel = new MantenimientoViewModel();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvVehiculo.ItemsSource = vehiculoViewModel.SelectAll();
            modificar = false;
        }
        public AddMantenimientos(Mantenimiento mantenimientoModificar)
        {
            InitializeComponent();
            this.mantenimientomodificar = mantenimientoModificar;
            this.mantenimientoViewModel = new MantenimientoViewModel();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.vehiculo = new Vehiculo();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvVehiculo.ItemsSource = vehiculoViewModel.SelectAll();
            if (mantenimientoModificar != null)
            {
                vehiculo.IdVehiculo = mantenimientoModificar.IdVehiculo;
                txtCoste.Text = mantenimientomodificar.Coste.ToString();
                txtDescripcion.Text = mantenimientomodificar.Descripcion.ToString();
                dtFechaReparacion.SelectedDate = mantenimientomodificar.Fecha;
                vehiculo.IdVehiculo = mantenimientomodificar.IdVehiculo;
                mantenimientomodificar.IdMantenimiento = mantenimientoModificar.IdMantenimiento;
                dgvVehiculo.IsReadOnly = true;
                dgvVehiculo.Visibility = Visibility.Hidden;
                modificar = true;
            }
        }

        private void btnAddMantenimientos_Click(object sender, RoutedEventArgs e)
        {
            bool validacion = true;
            Mantenimiento mantenimiento = new Mantenimiento();
            float numeroComprobado;

            if (txtCoste == null || txtCoste.Text == "" || txtDescripcion == null || txtDescripcion.Text == "" || dtFechaReparacion.SelectedDate == null || dtFechaReparacion == null)
            {
                MessageBox.Show("Todos los campos son obligatorios", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (!float.TryParse(txtCoste.Text, out numeroComprobado))
            {
                MessageBox.Show("No has introducido un numero", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (vehiculo == null)
            {
                MessageBox.Show("Debes seleccionar un vehiculo de la lista", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (validacion)
            {
                if (modificar)
                {
                    mantenimiento.IdMantenimiento = mantenimientomodificar.IdMantenimiento;
                    mantenimiento.IdVehiculo = vehiculo.IdVehiculo;
                    mantenimiento.Fecha = dtFechaReparacion.SelectedDate;
                    mantenimiento.Descripcion = txtDescripcion.Text;
                    mantenimiento.Coste = Convert.ToDouble(txtCoste.Text);
                    if (mantenimientoViewModel.ModificarMantenimiento(mantenimiento))
                    {
                        MessageBox.Show("Mantenimiento modificado correctamente");
                        Limpiarcampos();
                        mantenimiento = null;
                        vehiculo = null;
                    }
                }
                if (modificar == false)
                {
                    mantenimiento.IdVehiculo = vehiculo.IdVehiculo;
                    mantenimiento.Fecha = dtFechaReparacion.SelectedDate;
                    mantenimiento.Descripcion = txtDescripcion.Text;
                    mantenimiento.Coste = Convert.ToDouble(txtCoste.Text);
                    if (mantenimientoViewModel.InsertarMantenimiento(mantenimiento))
                    {
                        MessageBox.Show("Mantenimiento insertado correctamente");
                        Limpiarcampos();
                        mantenimiento = null;
                        vehiculo = null;
                    }
                }
            }
        }

        private void dgvVehiculo_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            vehiculo = (Vehiculo)dgvVehiculo.SelectedItem;
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

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Limpiarcampos()
        {
            txtCoste.Clear();
            txtDescripcion.Text = null;
            dtFechaReparacion.SelectedDate = null;
            mantenimientomodificar = null;
            vehiculo = null;
        }
    }
}
