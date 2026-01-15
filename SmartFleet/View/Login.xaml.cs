using SmartFleet.ViewModel;
using System.Windows;
using System.Windows.Input;
using SmartFleet.Model;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private EmpleadoViewModel empleadoViewModel;
        int contador = 0;
        public Login()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (contador < 3)
            {
                if (txtPass.Password.Equals(""))
                {
                    MessageBoxResult result = MessageBox.Show("Los campos están vacios", "Error", MessageBoxButton.OK, MessageBoxImage.Question);
                    contador++;
                }
                else
                {
                    Empleado empleado = new Empleado
                    {
                        Username = txtUser.Text.Trim(),
                        Passw = txtPass.Password.Trim()
                    };
                    empleadoViewModel = new EmpleadoViewModel();
                    empleado = empleadoViewModel.ValidarInicioSesion(empleado);
                    if (empleado != null && empleado.IdEmpleado != 0)
                    {
                        EmpleadoLogueado.IdEmpleado = empleado.IdEmpleado;
                        EmpleadoLogueado.Username = empleado.Username;
                        EmpleadoLogueado.Passw = empleado.Passw;
                        EmpleadoLogueado.NombreCompleto = empleado.Nombre + ' ' + empleado.Apellidos;
                        EmpleadoLogueado.FotoPerfil = empleado.FotoPerfil;

                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                        this.Close();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("El nombre o la contraseña son incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Question);
                        txtUser.SelectAll();
                        txtUser.Focus();
                        txtPass.Clear();
                        contador++;
                    }
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Has ingresado los datos mal demasiadas veces, la aplicacion se va a cerrar", "Error", MessageBoxButton.OK, MessageBoxImage.Question);
                Application.Current.Shutdown();
            }
        }
    }
}
