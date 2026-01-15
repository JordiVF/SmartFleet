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
    /// Lógica de interacción para AddCliente.xaml
    /// </summary>
    public partial class AddCliente : Window
    {
        private ClienteViewModel clienteViewModel;
        private bool modificar = false;
        private Cliente clienteModificar;
        public AddCliente()
        {
            InitializeComponent();
            modificar = false;
            this.clienteViewModel = new ClienteViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public AddCliente(Cliente clientemodificar)
        {
            InitializeComponent();
            this.clienteViewModel = new ClienteViewModel();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            if (clientemodificar != null)
            {
                this.clienteModificar = clientemodificar;
                txtNombre.Text = clientemodificar.Nombre;
                txtApellidos.Text = clientemodificar.Apellidos;
                txtEmail.Text = clientemodificar.Correo;
                txtTelefono.Text = clientemodificar.Telefono;
                txtDireccion.Text = clientemodificar.Direccion;
                txtCiudad.Text = clientemodificar.Ciudad;
                txtProvincia.Text = clientemodificar.Provincia;
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

        private void btnAddCliente_Click(object sender, RoutedEventArgs e)
        {

            bool validacion = true;
            Cliente cliente = new Cliente();
            string patternTelefono = @"^\+(?:[0-9] ?){6,14}[0-9]$";
            string patternEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Match matchEmail = Regex.Match(txtEmail.Text, patternEmail);
            Match matchTelefono = Regex.Match(txtTelefono.Text, patternTelefono);

            if (txtNombre == null || txtNombre.Text == "" || txtApellidos.Text == "" || txtApellidos == null || txtDireccion.Text == "" || txtDireccion == null ||
                txtCiudad == null || txtCiudad.Text == "" || txtProvincia.Text == "" || txtProvincia == null)
            {
                MessageBox.Show("Todos los campos son obligatorios", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if (matchEmail.Success && matchEmail.Value.Length == txtEmail.Text.Length) { }
                else
                {
                    MessageBox.Show("El correo electronico es invalido", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    validacion = false;
                }
            }
            if (validacion)
            {
                if (modificar)
                {
                    cliente.IdCliente = clienteModificar.IdCliente;
                    cliente.Nombre = txtNombre.Text;
                    cliente.Apellidos = txtApellidos.Text;
                    cliente.Correo = txtEmail.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Ciudad = txtCiudad.Text;
                    cliente.Provincia = txtProvincia.Text;
                    if (clienteViewModel.ModificarCliente(cliente))
                    {
                        MessageBox.Show("Cliente modificado correctamente");
                        Limpiarcampos();
                        cliente = null;
                    }
                }
                if (modificar == false)
                {
                    cliente.Nombre = txtNombre.Text;
                    cliente.Apellidos = txtApellidos.Text;
                    cliente.Correo = txtEmail.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Ciudad = txtCiudad.Text;
                    cliente.Provincia = txtProvincia.Text;
                    if (clienteViewModel.InsertarCliente(cliente))
                    {
                        MessageBox.Show("Cliente insertado correctamente");
                        Limpiarcampos();
                        cliente = null;
                    }
                }
            }
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Limpiarcampos()
        {
            txtNombre.Clear();
            txtApellidos.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCiudad.Clear();
            txtProvincia.Clear();
        }
    }
}
