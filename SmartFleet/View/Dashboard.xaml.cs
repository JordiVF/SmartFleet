using LiveCharts;
using LiveCharts.Wpf;
using SmartFleet.Model;
using SmartFleet.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmartFleet.View
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            CargarPieChartVehiculosMasAlquilados();
            CargarCartesianChartCosteMantenimientoMensual();
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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

        private void btn_Empleados_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "admin")
            {
                EmpleadosView empleadosView = new EmpleadosView();
                empleadosView.Show();
                this.Close();
            }
            if (txtUsername.Text !=  "admin")
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
        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart=(LiveCharts.Wpf.PieChart)chartPoint.ChartView;
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        private void CargarPieChartVehiculosMasAlquilados()
        {
            List<VehiculoAlquilerData> datos = DashboardViewModel.ObtenerDatosVehiculosAlquiler();

            SeriesCollection seriesCollection = new SeriesCollection();

            foreach (var dato in datos)
            {
                PieSeries serie = new PieSeries
                {
                    Title = $"{dato.Marca} {dato.Modelo}",
                    Values = new ChartValues<int> { dato.CantidadAlquileres },
                    DataLabels = true
                };

                seriesCollection.Add(serie);
            }

            VehiculosMasAlquilados.Series = seriesCollection;
        }

        private void CargarCartesianChartCosteMantenimientoMensual()
        {
            List<double> ListaCosteMantenimiento = new List<double>();
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Coste del Mantenimiento",
                    Values = DashboardViewModel.GetCosteMantenimientoMensual()
                }
            };

            Labels = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octumbre", "Noviembre", "Diciembre" };
            Values = value => value.ToString("N");

            DataContext = this;
        }
    }
}
