using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace KingsManTest.Windows.Employee
{
    /// <summary>
    /// Логика взаимодействия для EditServiceWindow.xaml
    /// </summary>
    public partial class EditServiceWindow : Window
    {
        DB.Service editService = null;

        public EditServiceWindow()
        {
            InitializeComponent();
        }

        public EditServiceWindow(DB.Service service)
        {
            InitializeComponent();

            editService = service;

            CmbServiceType.ItemsSource = ClassHelper.EF.Context.ServiceType.ToList();
            CmbServiceType.DisplayMemberPath = "TypeName";

            ImgAddServiceImage.Source = new BitmapImage(new Uri(service.Image));
            ImgAddServiceImage.Width = BrdrAddNewService.Width;
            ImgAddServiceImage.Height = BrdrAddNewService.Height;

            TbServiceName.Text = service.ServiceName;
            TbServiceDescription.Text = service.Description;
            TbServicePrice.Text = Convert.ToString(service.Price);

            CmbServiceType.SelectedItem = ClassHelper.EF.Context.ServiceType.ToList().Where(i => i.Id == service.ServiceTypeId).FirstOrDefault();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0)
            {
                (sender as TextBox).Foreground = Brushes.DarkGray;
                (sender as TextBox).Text = (string)(sender as TextBox).Tag;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length != 0 && (sender as TextBox).Text == (string)(sender as TextBox).Tag)
            {
                (sender as TextBox).Foreground = Brushes.Black;
                (sender as TextBox).Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            editService.ServiceName = TbServiceName.Text;
            editService.Description = TbServiceDescription.Text;
            editService.Price = Convert.ToDecimal(TbServicePrice.Text);
            editService.ServiceTypeId = (CmbServiceType.SelectedItem as DB.ServiceType).Id;

            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("OK");
            this.Close();
        }

        private void ImgAddServiceImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgAddServiceImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
