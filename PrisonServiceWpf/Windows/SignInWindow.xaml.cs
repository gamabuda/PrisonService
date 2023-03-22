using PrisonServiceWpf.Services;
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

namespace PrisonServiceWpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = DataBaseManager.GetEmployees().FirstOrDefault(x=>x.Number == NumberTB.Text && x.Password == PasswordTB.Password);
            if (user != null) 
            {
                DataBaseManager.Employee = user;
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Данный сотрудник не зарегестрирован в базе данных!");
            }
        }
    }
}
