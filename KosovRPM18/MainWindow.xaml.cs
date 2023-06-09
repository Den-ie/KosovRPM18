﻿using System;
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
        public MainWindow(User user)
        {
            InitializeComponent();
            db.Accountings.Load();
            DB.ItemsSource = db.Accountings.Local.ToBindingList();
            Title = "Текущий пользователь: " + user.Login;

            if (user.Rights == false)
            {
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;
                AddButtone.IsEnabled = false;
                ZaprosDelete.IsEnabled = false;
            }
            this.Height += 25;
        }

        AccountingEntities db = AccountingEntities.GetContext();

        private void AddButton(object sender, RoutedEventArgs e)
        {
            AddRecord f = new AddRecord();
            f.ShowDialog();
            DB.Focus();
            _accounting = db.Accountings.ToList();
            DB.ItemsSource = _accounting;
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
                _accounting = db.Accountings.ToList();
                DB.ItemsSource = _accounting;
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
                    _accounting = db.Accountings.ToList();
                    DB.ItemsSource = _accounting;
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

        List<Accounting> _accounting;

        private void FilterButton(object sender, RoutedEventArgs e)
        {
            _accounting = db.Accountings.ToList();
            var filtered = _accounting.Where(_accounting => _accounting.RecieverOrg.Contains(txtFilter.Text));
            DB.ItemsSource = filtered;
        }

        private void CancelFiltered(object sender, RoutedEventArgs e)
        {
            _accounting = db.Accountings.ToList();
            DB.ItemsSource = _accounting;
        }

        private void outtt(object sender, RoutedEventArgs e)
        {
            var gen = db.Database.SqlQuery<Accounting>("SELECT * FROM Accounting WHERE CommercialOrg = 1");
            DB.ItemsSource = gen.ToList();
        }

        private void MoreThan25k(object sender, RoutedEventArgs e)
        {
            var gen = db.Database.SqlQuery<Accounting>("SELECT * FROM Accounting WHERE TransferSumm >= 25000");
            DB.ItemsSource = gen.ToList();
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            db.Database.ExecuteSqlCommand("UPDATE Accounting SET TransferType='Пожертвование' WHERE TransferSumm < 25000");
            db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            _accounting = db.Accountings.ToList();
            DB.ItemsSource = _accounting;
        }

        private void Deleting(object sender, RoutedEventArgs e)
        {
            db.Database.ExecuteSqlCommand("DELETE FROM Accounting WHERE PointNumber > 10");
            db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            _accounting = db.Accountings.ToList();
            DB.ItemsSource = _accounting;
        }
    }
}
