using SmartFleet.Model;
using SmartFleet.ViewModel;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para MantenimientosView.xaml
    /// </summary>
    public partial class MantenimientosView : Window
    {
        private MantenimientoViewModel mantenimientoViewModel;
        public Mantenimiento mantenimiento;
        public MantenimientosView()
        {
            InitializeComponent();
            this.mantenimientoViewModel = new MantenimientoViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            dgvMantenimiento.ItemsSource = mantenimientoViewModel.SelectAll();
        }

        private void btnModificarMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            AddMantenimientos addMantenimientos = new AddMantenimientos(mantenimiento);
            addMantenimientos.Show();
            this.Close();
        }

        private void btnAddMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            AddMantenimientos addMantenimientos = new AddMantenimientos();
            addMantenimientos.Show();
            this.Close();
        }
        /// <summary>
        /// Funcion que carga el nombre de usuario y su imagen en sus respectivos campos de la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Funcion que recarga la ventana actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Funcion que te envia a la ventana principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboardView = new Dashboard();
            dashboardView.Show();
            this.Close();
        }
        /// <summary>
        /// Funcion que te desconecta y te devuelve al Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Funcion que busca un usuario en el datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            string busqueda = txtBuscar.Text;

            if (txtBuscar.Text == "" || txtBuscar.Text == null)
            {
                dgvMantenimiento.ItemsSource = mantenimientoViewModel.SelectAll();
                MessageBox.Show("Puedes buscar por Descripcion, Matricula, Marca y Modelo");
            }
            else
            {
                dgvMantenimiento.ItemsSource = mantenimientoViewModel.SelectBuscarMantenimiento(busqueda);
            }
        }
        private void btn_Clientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesView clientesView = new ClientesView();
            clientesView.ShowDialog();
            this.Close();
        }

        private void btn_Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            VehiculosView vehiculosView = new VehiculosView();
            vehiculosView.ShowDialog();
            this.Close();
        }

        private void btn_Mantenimiento_Click(object sender, RoutedEventArgs e)
        {
            MantenimientosView mantenimientosView = new MantenimientosView();
            mantenimientosView.ShowDialog();
            this.Close();
        }

        private void btn_Alquiler_Click(object sender, RoutedEventArgs e)
        {
            AlquilerView alquilerView = new AlquilerView();
            alquilerView.ShowDialog();
            this.Close();
        }

        private void dgvMantenimiento_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            mantenimiento = (Mantenimiento)dgvMantenimiento.SelectedItem;
        }

        private void btnEliminarMantenimiento_Click(object sender, RoutedEventArgs e)
        {
            Mantenimiento mantenimiento = (Mantenimiento)dgvMantenimiento.SelectedItem;
            if (mantenimiento != null)
            {
                if (MessageBox.Show("¿Estas seguro que quieres borrar este Mantenimiento?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    if (mantenimientoViewModel.BorrarMantenimiento(mantenimiento))
                    {
                        MessageBox.Show("Empleado borrado correctamente");
                    }
                }
                dgvMantenimiento.ItemsSource = mantenimientoViewModel.SelectAll();
            }
        }
    }
}
