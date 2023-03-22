using Microsoft.Win32;
using PrisonService.Data;
using PrisonService.Data.Shared;
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
using System.Xml.Linq;

namespace PrisonServiceWpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для PrisonerWindow.xaml
    /// </summary>
    public partial class PrisonerWindow : Window
    {
        public Prisoner Prisoner { get; set; }
        public PrisonerWindow(Prisoner prisoner)
        {
            InitializeComponent();

            Prisoner = prisoner;

            this.DataContext = Prisoner;

            EnableItems();
            LoadImgBtn.Visibility = Visibility.Collapsed;
        }
        public PrisonerWindow(Prisoner prisoner, bool isEditAllowed)
        {
            InitializeComponent();

            Prisoner = prisoner;
            _isEditAllowed = isEditAllowed;

            this.DataContext = Prisoner;

            EditBtn.Content = "Cохранить";
        }

        private bool _isEditAllowed = false;

        private void EnableItems()
        {
            foreach (object obj in MainSP.Children)
            {
                if (obj is TextBox)
                {
                    (obj as TextBox).IsEnabled = false;
                }

                if (obj is DatePicker)
                {
                    (obj as DatePicker).IsEnabled = false;
                }

                if (obj is Grid)
                {
                    foreach (object containerObj in (obj as Grid).Children)
                    {
                        if (containerObj is TextBox)
                        {
                            (containerObj as TextBox).IsEnabled = false;
                        }

                        if (containerObj is DatePicker)
                        {
                            (containerObj as DatePicker).IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Вы действительно хотите удалить запись про {Prisoner.Fullname} из базы данных?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditAllowed)
            {
                this.Close();
                new PrisonerWindow(Prisoner, true).ShowDialog();
                
                return;
            }

            if (!GenereatorStub.Prisoners.Contains(Prisoner))
            {
                GenereatorStub.Prisoners.Add(Prisoner);

                this.Close();
                return;
            }
        }

        private void LoadImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                ProfileImg.Source = new BitmapImage(new Uri(op.FileName));
            }
        }
    }
}
