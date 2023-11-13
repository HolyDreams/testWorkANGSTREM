using System.Text.RegularExpressions;
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
using testWorkANGSTREM.Models;
using testWorkANGSTREM.Startup;
using testWorkANGSTREM.Methods;

namespace testWorkANGSTREM.Views
{
    /// <summary>
    /// Логика взаимодействия для wAddNomen.xaml
    /// </summary>
    public partial class wAddNomen : Window
    {
        List<IdShortNameStruct> bomList;

        public wAddNomen()
        {
            InitializeComponent();
            bomList = StaticParams.GetBom();
            comboBom.ItemsSource = bomList.Select(a => a.Name);
        }
        private void butSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textName.Text))
            {
                MessageBox.Show("Нужно ввести имя!");
                return;
            }
            else if (string.IsNullOrEmpty(textPrice.Text))
            {
                MessageBox.Show("Нужно ввести цену!");
                return;
            }
            else if (comboBom.SelectedIndex < 0) 
            {
                MessageBox.Show("Нужно ввести единицу измерения!");
                return;
            }

            var editor = new NomenWork();
            editor.AddNomen(new NomenStruct()
            {
                Name = textName.Text,
                Price = double.Parse(textPrice.Text),
                BomCode = bomList[comboBom.SelectedIndex].ID,
            });

            this.Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9.-]+$");
            e.Handled = !regex.IsMatch(e.Text) || (e.Text.Contains('.') && ((TextBox)e.Source).Text.Where(a => a == '.').Count() > 0);
        }
    }
}
