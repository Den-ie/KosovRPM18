using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KosovRPM18
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Height += 25;
        }

        AccountingEntities db = AccountingEntities.GetContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Accountings.Load();
            DB.ItemsSource = db.Accountings.Local.ToBindingList();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            AddRecord f = new AddRecord();
            f.ShowDialog();
            DB.Focus();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            int indexRow = DB.SelectedIndex;
            if (indexRow != -1)
            {
                Accounting row = (Accounting)DB.Items[indexRow];
                Data.Id = row.PointNumber;
                EditRecord f = new EditRecord();
                f.ShowDialog();
                DB.Items.Refresh();
                DB.Focus();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Accounting row = (Accounting)DB.SelectedItems[0];
                    db.Accountings.Remove(row);
                    db.SaveChanges();
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выбирите запись");
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Косов Даниил ИСП-34 \nВариант №4 Журнал регистрации расходов в бухгалтерии. База данных должна содержать следующую \r\nинформацию: номер пункта, дату перечисления, название организации-получателя, ее \r\nадрес и сведения о том, является ли организация коммерческой, а также вид затрат \r\nперечисления и общую сумму перечисления.");
        }

        private void FindButton(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < DB.Items.Count; i++)
            {
                var row = (Accounting)DB.Items[i];
                string findContent = row.RecieverOrg;
                try
                {
                    if (findContent != null && findContent.Contains(txtFind.Text))
                    {
                        object item = DB.Items[i];
                        DB.SelectedItem = item;
                        DB.ScrollIntoView(item);
                        DB.Focus();
                        break;
                    }
                }
                catch { }
            }
        }

        List<Accounting> _teammember;

        private void FilterButton(object sender, RoutedEventArgs e)
        {
            _teammember = db.Accountings.ToList();
            var filtered = _teammember.Where(_teammember => _teammember.RecieverOrg.Contains(txtFilter.Text));
            DB.ItemsSource = filtered;
        }

        private void CancelFiltered(object sender, RoutedEventArgs e)
        {
            DB.ItemsSource = _teammember;
        }
    }
}
