using PrisonService.Data;
using PrisonService.Data.Shared;
using PrisonServiceWpf.Windows;
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

namespace PrisonServiceWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Adress> Adress { get; set; } = GenereatorStub.Adresses;
        public List<string> State { get; set; } = GenereatorStub.Prisoners.Select(x => x.State).ToList();
        public List<Prison> Prison { get; set; } = GenereatorStub.Prisons; 
        public List<Prisoner> Prisoners { get; set; } = GenereatorStub.Prisoners;
        public Employee Employee { get; set; } = Employee.Create("42452","Палков Михаил Шизович", "Надзератель", "Черный Дельфин");

        public MainWindow()
        {
            InitializeComponent();

            MainLV.DataContext = Prisoners;
            MainLV.ItemsSource = Prisoners;

            PrisonCB.DataContext = Prison;
            PrisonCB.ItemsSource = Prison;

            StateCB.DataContext = State;
            StateCB.ItemsSource = State;

            AdressCB.DataContext = Adress;
            AdressCB.ItemsSource = Adress;

            SurnameLb.Content = Employee.Fullname.Split(' ').First();
            ProfileGrid.DataContext = Employee;

            new SignInWindow().Show();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentLst = Prisoners;

            if(SearchTB.Text != String.Empty) 
            {
                currentLst = Prisoners.Where(x => x.Fullname.Contains(SearchTB.Text)).ToList();
            }

            MainLV.ItemsSource = currentLst;
        }

        private void MenuItemView_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(MainLV.SelectedItem as Prisoner).ShowDialog();
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(MainLV.SelectedItem as Prisoner, true).ShowDialog();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Вы действительно хотите удалить запись про {(MainLV.SelectedItem as Prisoner).Fullname} из базы данных?", 
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            new SignInWindow().Show();
            this.Close(); 
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(Prisoner.CreateEmpty(), true).ShowDialog();

            Prisoners = GenereatorStub.Prisoners;
            MainLV.ItemsSource = Prisoners;
        }

        private void PrisonCB_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
