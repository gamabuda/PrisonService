using Microsoft.Win32;
using PrisonService.Data;
using PrisonService.Data.Shared;
using PrisonServiceWpf.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Prison> Prison { get; set; } = DataBaseManager.GetPrisons();
        public List<Adress> Adress { get; set; } = DataBaseManager.GetAdresses();
        public PrisonerWindow(Prisoner prisoner)
        {
            InitializeComponent();

            PrisonTB.DataContext = Prison;
            PrisonTB.ItemsSource = Prison;
            AdressTB.DataContext = Adress;
            AdressTB.ItemsSource = Adress;

            Prisoner = prisoner;

            this.DataContext = Prisoner;

            EnableItems();
            LoadImgBtn.Visibility = Visibility.Collapsed;
            if (Prisoner.Photo != null)
                ProfileImg.Visibility = Visibility.Collapsed;
        }
        public PrisonerWindow(Prisoner prisoner, bool isEditAllowed)
        {
            InitializeComponent();

            PrisonTB.DataContext = Prison;
            PrisonTB.ItemsSource = Prison;
            AdressTB.DataContext = Adress;
            AdressTB.ItemsSource = Adress;

            Prisoner = prisoner;
            _isEditAllowed = isEditAllowed;

            this.DataContext = Prisoner;

            EditBtn.Content = "Cохранить";
            if (Prisoner.Photo != null)
                ProfileImg.Visibility = Visibility.Collapsed;
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

                if (obj is ComboBox)
                {
                    (obj as ComboBox).IsEnabled = false;
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

                        if (containerObj is ComboBox)
                        {
                            (containerObj as ComboBox).IsEnabled = false;
                        }
                    }
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show($"Вы действительно хотите удалить запись про {Prisoner.Fullname} из базы данных?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if(result == MessageBoxResult.Yes)
            {
                DataBaseManager.TryRemovePrisoner(Prisoner);
                this.Close();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditAllowed)
            {
                this.Close();
                new PrisonerWindow(Prisoner, true).ShowDialog();
                
                return;
            }

            if (DataBaseManager.GetPrisoners().FirstOrDefault(x => x.Passport == Prisoner.Passport) == null)
            {
                DataBaseManager.TryAddPrisoner(Prisoner);

                this.Close();
                return;
            }
            else
            {
                DataBaseManager.TryUpdatePrisoner(Prisoner);
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
                Prisoner.Photo = GetIMGFolder(new BitmapImage(new Uri(op.FileName)));
            }
        }

        private byte[] GetIMGFolder(BitmapImage bi)
        {
            PngBitmapEncoder pe = new PngBitmapEncoder();
            MemoryStream ms = new MemoryStream();
            StringBuilder sb = new StringBuilder();
            pe.Frames.Add(BitmapFrame.Create(bi));
            pe.Save(ms);
            return ms.ToArray();
        }
    }
}
