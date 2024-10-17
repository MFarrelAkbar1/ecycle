using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ecycle
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DashboardPembeli_Click(object sender, RoutedEventArgs e)
        {
            Window1 dashboardPembeli = new Window1();
            dashboardPembeli.Show();
            this.Close(); // Opsional: Menutup MainWindow
        }

        private void EdukasiDaurUlang_Click(object sender, RoutedEventArgs e)
        {
            Window2 edukasiDaurUlang = new Window2();
            edukasiDaurUlang.Show();
            this.Close(); // Opsional: Menutup MainWindow
        }

        private void RekomendasiProduk_Click(object sender, RoutedEventArgs e)
        {
            Window3 rekomendasiProduk = new Window3();
            rekomendasiProduk.Show();
            this.Close(); // Opsional: Menutup MainWindow
        }

        private void FiturPenjual_Click(object sender, RoutedEventArgs e)
        {
            Window4 fiturPenjual = new Window4();
            fiturPenjual.Show();
            this.Close(); // Opsional: Menutup MainWindow
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
