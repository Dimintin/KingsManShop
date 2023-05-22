using KingsManTest.DB;
using KingsManTest.Windows.Employee;
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

namespace KingsManTest.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для EditClientWindow.xaml
    /// </summary>
    public partial class EditClientWindow : Window
    {
        DB.Client newClient = null;
        public EditClientWindow()
        {
            InitializeComponent();
        }

        public EditClientWindow(DB.Client client)
        {
            InitializeComponent();

            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbGender.DisplayMemberPath = "GenderName";

            TbFirstName.Text = client.FirstName;
            TbLastName.Text = client.LastName;
            TbPatronymic.Text = client.Patronymic;
            TbNewLogin.Text = client.Login;
            TbNewPassword.Text = client.Password;
            TbPhone.Text = client.Phone;
            CmbGender.SelectedItem = ClassHelper.EF.Context.Gender.ToList().Where(i => i.GenderCode == client.GenderCode).FirstOrDefault();

            newClient = client;
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
            newClient.FirstName = TbFirstName.Text;
            newClient.LastName = TbLastName.Text;
            newClient.Patronymic = TbPatronymic.Text;
            newClient.Phone = TbPhone.Text;
            newClient.Login = TbNewLogin.Text;
            newClient.Password = TbNewPassword.Text;
            newClient.GenderCode = (CmbGender.SelectedItem as DB.Gender).GenderCode;

            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("OK");
            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
