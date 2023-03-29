using PrisonService.Data;
using PrisonService.Data.Shared;
using PrisonServiceWpf.Services;
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
        public List<Adress> Adress { get; set; } = DataBaseManager.GetAdresses();
        public List<string> State { get; set; } = GenereatorStub.Prisoners.Select(x => x.State).ToList();
        public List<Prison> Prison { get; set; } = DataBaseManager.GetPrisons(); 
        public List<Prisoner> Prisoners { get; set; } = DataBaseManager.GetPrisoners();
        public Employee Employee { get; set; } = DataBaseManager.Employee;

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
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentLst = Prisoners;

            if(SearchTB.Text != String.Empty) 
            {
                currentLst = Prisoners.Where(x => x.Fullname.Contains(SearchTB.Text)).ToList();

                PrisonCB.SelectedItem = null;
                StateCB.SelectedItem = null;
                AdressCB.SelectedItem = null;
            }

            MainLV.ItemsSource = currentLst;
        }

        private void MenuItemView_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(MainLV.SelectedItem as Prisoner).ShowDialog();
            Prison = DataBaseManager.GetPrisons();
            PrisonCB.ItemsSource = Prison;
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(MainLV.SelectedItem as Prisoner, true).ShowDialog();
            Prison = DataBaseManager.GetPrisons();
            PrisonCB.ItemsSource = Prison;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы действительно хотите удалить запись про {(MainLV.SelectedItem as Prisoner).Fullname} из базы данных?", 
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if(result == MessageBoxResult.Yes)
            {
                DataBaseManager.TryRemovePrisoner(MainLV.SelectedItem as Prisoner);
                Prisoners = DataBaseManager.GetPrisoners();
                MainLV.ItemsSource = Prisoners;
            } 
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            new SignInWindow().Show();
            this.Close(); 
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)
        {
            new PrisonerWindow(Prisoner.CreateEmpty(), true).ShowDialog();

            Prisoners = DataBaseManager.GetPrisoners();
            MainLV.ItemsSource = Prisoners;
        }

        private void PrisonCB_DropDownClosed(object sender, EventArgs e)
        {
            if(PrisonCB.SelectedItem == null)
                return;

            Prisoners = DataBaseManager.GetPrisoners();
            MainLV.ItemsSource = Prisoners.Where(x => x.Prison.Name == PrisonCB.Text);

            AdressCB.SelectedItem = null;
            StateCB.SelectedItem = null;
            SearchTB.Text = null;
        }

        private void StateCB_DropDownClosed(object sender, EventArgs e)
        {
            if (StateCB.SelectedItem == null)
                return;

            Prisoners = DataBaseManager.GetPrisoners();
            MainLV.ItemsSource = Prisoners.Where(x => x.State == StateCB.Text);

            PrisonCB.SelectedItem = null;
            AdressCB.SelectedItem = null;
            SearchTB.Text = null;
        }

        private void AdressCB_DropDownClosed(object sender, EventArgs e)
        {
            if (AdressCB.SelectedItem == null)
                return;

            Prisoners = DataBaseManager.GetPrisoners();
            MainLV.ItemsSource = Prisoners.Where(x => x.Adress.Title == AdressCB.Text);

            PrisonCB.SelectedItem = null;
            StateCB.SelectedItem = null;
            SearchTB.Text = null;
        }
    }
}
