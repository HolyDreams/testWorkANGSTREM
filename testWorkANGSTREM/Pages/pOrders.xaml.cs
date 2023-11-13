using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using testWorkANGSTREM.Methods;
using testWorkANGSTREM.Models;
using testWorkANGSTREM.Views;

namespace testWorkANGSTREM.Pages
{
    /// <summary>
    /// Логика взаимодействия для pOrders.xaml
    /// </summary>
    public partial class pOrders : UserControl
    {
        List<IdGuidNameStruct> mainList;
        public pOrders()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = textSearch.Text.ToLower();
            var searchList = mainList.Where(a => a.Name.ToLower().Contains(search)).ToList();

            gridControl1.ItemsSource = searchList;

            printTable();
        }
        private void butNewNomen_Click(object sender, RoutedEventArgs e)
        {
            var order = new wOrder();
            order.ShowDialog();

            loadTable();
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (IdGuidNameStruct)gridControl1.SelectedItem;
            if (row == null)
                return;

            var order = new wOrder(row.Name, row.ID);
            order.ShowDialog();

            loadTable();
        }
        private void loadTable()
        {
            var loader = new OrderWork();
            mainList = loader.GetOrders();

            gridControl1.ItemsSource = mainList;

            printTable();
        }
        private void printTable()
        {
            var idCol = gridControl1.Columns.First(a => (string)a.Header == "ID");
            idCol.Visibility = Visibility.Hidden;
        }

    }
}
