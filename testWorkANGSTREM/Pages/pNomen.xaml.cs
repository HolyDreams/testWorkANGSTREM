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

namespace testWorkANGSTREM.Views
{
    /// <summary>
    /// Логика взаимодействия для pNomen.xaml
    /// </summary>
    public partial class pNomen : UserControl
    {
        List<NomenStruct> mainList;
        public pNomen()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        private void butNewNomen_Click(object sender, RoutedEventArgs e)
        {
            var newNomen = new wAddNomen();
            newNomen.ShowDialog();

            loadTable();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = textSearch.Text.ToLower();
            var searchList = mainList.Where(a => a.Name.ToLower().Contains(search)).ToList();

            gridControl1.ItemsSource = searchList;

            printTable();
        }

        private void loadTable()
        {
            var loader = new NomenWork();
            mainList = loader.GetNomen();

            gridControl1.ItemsSource = mainList;

            printTable();
        }
        private void printTable()
        {
            foreach (var item in gridControl1.Columns)
            {
                switch ((string)item.Header)
                {
                    case "ID":
                        item.Visibility = Visibility.Hidden;
                        continue;
                    case "Name":
                        item.DisplayIndex = 0;
                        continue;
                    case "Price":
                        item.DisplayIndex = 1;
                        continue;
                    case "BomCode":
                        item.Visibility= Visibility.Hidden;
                        continue;
                    case "Bom":
                        item.DisplayIndex = 2;
                        continue;
                    default:
                        continue;
                }
            }
        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {
            var row = (NomenStruct)gridControl1.SelectedItem;
            if (row == null)
                return;

            var answer = MessageBox.Show(string.Join(" ", "Вы уверены что хотите удалить", row.Name, "?"), "Подтверждение удаления", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                var editor = new NomenWork();
                editor.DelNomen(row.ID);
            }

            loadTable();
        }
    }
}
