using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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

namespace KosovRPM18
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public AddRecord()
        {
            InitializeComponent();
            this.Height += 25;
        }

        AccountingEntities db = AccountingEntities.GetContext();
        Accounting p1 = new Accounting();

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add(object sender, RoutedEventArgs e)

        {
            StringBuilder errors = new StringBuilder();

            if (DatePicker.Text.Length == 0)
                errors.AppendLine("Выберите дату приема");
            if (RecivierOrg.Text.Length == 0)
                errors.AppendLine("Введите получателя");
            if (Adress.Text.Length == 0)
                errors.AppendLine("Введите адресс");
            if (CommercOrg.Text != "Да" && CommercOrg.Text != "Нет")
                errors.AppendLine("Выберите коммерческая ли организация");
            if (Type.Text.Length == 0)
                errors.AppendLine("Введите число пасов");
            if (!Int32.TryParse(Summ.Text, out int x))
                errors.AppendLine("Введите штрафное время");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            p1.TransferDate = DatePicker.SelectedDate;
            p1.RecieverOrg = RecivierOrg.Text;
            p1.OrgAdress = Adress.Text;
            if (CommercOrg.Text == "Да")
                p1.CommercialOrg = true;
            else
                p1.CommercialOrg = false;
            p1.TransferType = Type.Text;
            p1.TransferSumm = x;

            try
            {
                db.Accountings.Add(p1);
                db.SaveChanges();
                MessageBox.Show("Сохранено");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
