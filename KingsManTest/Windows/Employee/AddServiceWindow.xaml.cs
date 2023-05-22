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
using static System.Net.Mime.MediaTypeNames;

namespace KingsManTest.Windows.Employee
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        private string imagePath = null;

        public AddServiceWindow()
        {
            InitializeComponent();

            CmbServiceType.ItemsSource = ClassHelper.EF.Context.ServiceType.ToList();
            CmbServiceType.DisplayMemberPath = "TypeName";
            CmbServiceType.SelectedItem = 0;
        }

        private void ImgAddServiceImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgAddServiceImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                imagePath = openFileDialog.FileName;
            }
            ImgAddServiceImage.Width = BrdrAddNewService.Width;
            ImgAddServiceImage.Height = BrdrAddNewService.Height;
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
            if (string.IsNullOrEmpty(TbServiceName.Text) || string.IsNullOrEmpty(TbServiceDescription.Text) || string.IsNullOrEmpty(TbServicePrice.Text))
            {
                MessageBox.Show("Нет");
                return;
            }

            if (TbServiceName.Text == Convert.ToString(TbServiceName.Tag) || TbServiceDescription.Text == Convert.ToString(TbServiceDescription.Tag) ||
                TbServicePrice.Text == Convert.ToString(TbServicePrice.Tag))
            {
                MessageBox.Show("Нет");
                return;
            }

            if (imagePath == null)
            {
                MessageBox.Show("Нет");
                return;
            }
            DB.Service newService = new DB.Service();

            newService.ServiceName = TbServiceName.Text;
            newService.Description = TbServiceDescription.Text;
            newService.Price = Convert.ToDecimal(TbServicePrice.Text);
            newService.Image = imagePath;
            try
            {
                Convert.ToDecimal(TbServicePrice.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Нет");
                return;
            }
            newService.ServiceTypeId = (CmbServiceType.SelectedItem as DB.ServiceType).Id;

            ClassHelper.EF.Context.Service.Add(newService);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("ОК!");

            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
