using System.Windows;
using System.Windows.Controls;
using testWorkANGSTREM.Pages;

namespace testWorkANGSTREM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void butNomenclatura_Click(object sender, RoutedEventArgs e)
        {
            var nomen = new pNomen();
            mainContentControl.Content = nomen;
        }

        private void butOrder_Click(object sender, RoutedEventArgs e)
        {
            var orders = new pOrders();
            mainContentControl.Content = orders;
        }
    }
}
