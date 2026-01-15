using SmartFleet.Model;
using SmartFleet.ViewModel;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para AddAlquiler.xaml
    /// </summary>
    public partial class AddAlquiler : Window
    {
        private Vehiculo vehiculo;
        private Cliente cliente;
        private Alquiler alquilerModificar;
        private AlquilerViewModel alquilerViewModel;
        private ClienteViewModel clienteViewModel;
        private VehiculoViewModel vehiculoViewModel;
        private bool modificar = false;
        public AddAlquiler()
        {
            InitializeComponent();
            this.alquilerViewModel = new AlquilerViewModel();
            this.clienteViewModel = new ClienteViewModel();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvCliente.ItemsSource = clienteViewModel.SelectAll();
            dgvVehiculo.ItemsSource = vehiculoViewModel.SelectAll();
            modificar = false;
        }
        public AddAlquiler(Alquiler alquilermodificar)
        {
            InitializeComponent();
            this.alquilerModificar = alquilermodificar;
            this.alquilerViewModel = new AlquilerViewModel();
            this.clienteViewModel = new ClienteViewModel();
            this.vehiculoViewModel = new VehiculoViewModel();
            this.vehiculo = new Vehiculo();
            this.cliente = new Cliente();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvCliente.ItemsSource = clienteViewModel.SelectAll();
            dgvVehiculo.ItemsSource = vehiculoViewModel.SelectAll();
            if (alquilermodificar != null)
            {
                cliente.IdCliente = alquilermodificar.IdCliente;
                vehiculo.IdVehiculo = alquilermodificar.IdVehiculo;
                txtPrecioTotal.Text = alquilermodificar.PrecioTotal.ToString();
                dtFechaInicio.SelectedDate = alquilermodificar.FechaInicio;
                dtFechaFin.SelectedDate = alquilermodificar.FechaFin;
                cliente.IdCliente = alquilermodificar.IdCliente;
                vehiculo.IdVehiculo = alquilermodificar.IdVehiculo;
                alquilerModificar.IdAlquiler = alquilermodificar.IdAlquiler;
                modificar = true;
            }
        }

        private void dgvCliente_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            cliente = (Cliente)dgvCliente.SelectedItem;
        }

        private void dgvVehiculo_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            vehiculo = (Vehiculo)dgvVehiculo.SelectedItem;
        }

        private void btnAddAlquiler_Click(object sender, RoutedEventArgs e)
        {
            bool validacion = true;
            Alquiler alquiler = new Alquiler();
            double numeroComprobado;

            if (txtPrecioTotal == null || txtPrecioTotal.Text == "" || dtFechaFin.SelectedDate == null || dtFechaFin == null || dtFechaInicio.SelectedDate == null || dtFechaInicio == null)
            {
                MessageBox.Show("Todos los campos son obligatorios", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if(vehiculo == null)
            {
                MessageBox.Show("Debes seleccionar un vehiculo de la lista", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (cliente == null)
            {
                MessageBox.Show("Debes seleccionar un cliente de la lista", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (!Double.TryParse(txtPrecioTotal.Text, out numeroComprobado))
            {
                MessageBox.Show("No has introducido un numero", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                validacion = false;
            }
            if (validacion)
            {
                if (modificar)
                {
                    alquiler.IdAlquiler = alquilerModificar.IdAlquiler;
                    alquiler.PrecioTotal = Convert.ToDouble(txtPrecioTotal.Text);
                    alquiler.FechaInicio = dtFechaInicio.SelectedDate;
                    alquiler.FechaFin = dtFechaFin.SelectedDate;
                    alquiler.IdCliente = cliente.IdCliente;
                    alquiler.IdVehiculo = vehiculo.IdVehiculo;
                    if (alquilerViewModel.ModificarAlquiler(alquiler))
                    {
                        MessageBox.Show("Alquiler modificado correctamente");
                        Limpiarcampos();
                        alquiler = null;
                        cliente = null;
                        vehiculo = null;
                    }
                }
                if (modificar == false)
                {
                    alquiler.PrecioTotal = Convert.ToDouble(txtPrecioTotal.Text);
                    alquiler.FechaInicio = dtFechaInicio.SelectedDate;
                    alquiler.FechaFin = dtFechaFin.SelectedDate;
                    alquiler.IdCliente = cliente.IdCliente;
                    alquiler.IdVehiculo = vehiculo.IdVehiculo;
                    if (alquilerViewModel.InsertarAlquiler(alquiler))
                    {
                        MessageBox.Show("Alquiler insertado correctamente");
                        Limpiarcampos();
                        alquiler = null;
                        cliente = null;
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

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Limpiarcampos()
        {
            txtPrecioTotal.Clear();
            dtFechaInicio.SelectedDate = null;
            dtFechaFin.SelectedDate = null;
            cliente = null;
            vehiculo = null;
        }
    }
}
