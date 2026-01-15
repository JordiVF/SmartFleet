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
    /// Lógica de interacción para ClientesView.xaml
    /// </summary>
    public partial class ClientesView : Window
    {
        private ClienteViewModel clienteViewModel;
        public Cliente cliente;
        public ClientesView()
        {
            InitializeComponent();
            this.clienteViewModel = new ClienteViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvClientes.ItemsSource = clienteViewModel.SelectAll();
        }

        private void btnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            AddCliente addCliente = new AddCliente();
            addCliente.Show();
            this.Close();
        }

        private void btnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            AddCliente addCliente = new AddCliente(cliente);
            addCliente.Show();
            this.Close();
        }

        private void btn_Empleados_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "admin")
            {
                EmpleadosView empleadosView = new EmpleadosView();
                empleadosView.Show();
                this.Close();
            }
            if (txtUsername.Text != "admin")
            {
                MessageBox.Show("Solo el admin puede acceder a los empleados.");
            }
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
                this.Close(); ;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres salir?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Application.Current.Shutdown();
            }
        }

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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            string busqueda = txtBuscar.Text;

            if (txtBuscar.Text == "" || txtBuscar.Text == null)
            {
                dgvClientes.ItemsSource = clienteViewModel.SelectAll();
                MessageBox.Show("Puedes buscar por Nombre o Apellidos");
            }
            else
            {
                dgvClientes.ItemsSource = clienteViewModel.SelectBuscarClientes(busqueda);
            }
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void dgvClientes_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            cliente = (Cliente)dgvClientes.SelectedItem;
        }

        private void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = (Cliente)dgvClientes.SelectedItem;
            if (cliente != null)
            {
                if (MessageBox.Show("¿Estas seguro que quieres borrar al cliente " + cliente.Nombre + " " + cliente.Apellidos + " ?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    clienteViewModel.DeleteCliente(cliente.IdCliente);
                }
                dgvClientes.ItemsSource = clienteViewModel.SelectAll();
            }
        }
    }
}
