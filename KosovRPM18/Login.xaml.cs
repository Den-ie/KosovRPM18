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
using System.Windows.Shapes;
using System.Data.Entity;

namespace KosovRPM18
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private AccountingEntities _db = AccountingEntities.GetContext();

        public Login()
        {
            InitializeComponent();
        }

        public User User { get; set; }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLogin.Text)) return;
            if (string.IsNullOrEmpty(txtPassword.Password)) return;

            var user = _db.Users.Where(p => p.Login == txtLogin.Text).FirstOrDefault();

            if (user == null)
            {
                message.Text = "Пользователя с таким логином не существует";
                return;
            }

            if (user.Password.ToString() != txtPassword.Password.ToString())
            {
                message.Text = "Пароль не верен";
                return;
            }

            MainWindow mainWindow = new MainWindow(user);
            User = user;
            mainWindow.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
