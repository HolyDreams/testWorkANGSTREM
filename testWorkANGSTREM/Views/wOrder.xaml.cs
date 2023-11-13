using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using testWorkANGSTREM.Methods;
using testWorkANGSTREM.Models;

namespace testWorkANGSTREM.Views
{
    /// <summary>
    /// Логика взаимодействия для wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        Guid? orderID;
        List<BasketStruct> mainList;
        public wOrder(string? name = null, Guid? orderID = null)
        {
            InitializeComponent();
            this.orderID = orderID;
            windowOrder.Title = name ?? "Создание нового заказа";
        }

        private void butNewNomen_Click(object sender, RoutedEventArgs e)
        {
            addNomen();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!orderID.HasValue)
                return;

            loadTable();
        }

        private void textItog_TextChanged(object sender, TextChangedEventArgs e)
        {
            double sum;
            if (!double.TryParse(textItog.Text, out sum))
                return;

            textNDS.Text = (sum * 0.05).ToString();
        }

        private void loadTable()
        {
            var loader = new BasketWork();
            mainList = loader.GetBasket(orderID.Value);

            gridControl.ItemsSource = mainList;
            textItog.Text = mainList.Sum(a => a.SumPrice).ToString();
            printTable();
        }
        private void printTable()
        {
            foreach (var item in gridControl.Columns)
            {
                switch (item.Header)
                {
                    case "BomCode":
                        item.Visibility = Visibility.Hidden;
                        continue;
                    case "BasketID":
                        item.Visibility = Visibility.Hidden;
                        continue;
                    case "ID":
                        item.DisplayIndex = 0;
                        continue;
                    case "Name":
                        item.DisplayIndex = 1;
                        continue;
                    case "Bom":
                        item.DisplayIndex = 2;
                        continue;
                    case "Price":
                        item.DisplayIndex = 3;
                        continue;
                    case "Count":
                        item.DisplayIndex = 4;
                        continue;
                    case "SumPrice":
                        item.DisplayIndex = 5;
                        continue;
                    default:
                        continue;
                }
            }
        }
        private void addNomen()
        {
            int id;
            int count;
            if (!int.TryParse(textID.Text, out id))
            {
                MessageBox.Show("Невалидный ID");
                return;
            }
            else if (!int.TryParse(textCount.Text, out count))
            {
                MessageBox.Show("Нужно ввести кол-во!");
                return;
            }

            var checkNomen = new NomenWork();
            if (!checkNomen.CheckNomen(id))
            {
                MessageBox.Show("Такого товара не существует, попробуйте другой код!");
                return;
            }

            if (!orderID.HasValue)
            {
                var creatOrder = new OrderWork();
                orderID = creatOrder.CreateOrder();
                if (!orderID.HasValue)
                    return;
            }

            var editor = new BasketWork();
            if (mainList.Any(a => a.ID == id))
            {
                var row = mainList.First(a => a.ID == id);
                editor.EditBasket(row.BasketID, row.Count + count);
            }
            else
                editor.AddBasket(orderID.Value, id, count);

            loadTable();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
