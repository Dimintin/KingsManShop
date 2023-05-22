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

namespace KingsManTest.Windows.Client
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbGender.DisplayMemberPath = "GenderName";
            CmbGender.SelectedItem = 0;
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
            if (string.IsNullOrEmpty(TbLastName.Text) || string.IsNullOrEmpty(TbFirstName.Text) || string.IsNullOrEmpty(TbPhone.Text) ||
                string.IsNullOrEmpty(TbNewLogin.Text) || string.IsNullOrEmpty(TbNewPassword.Text))
            {
                MessageBox.Show("Нельзя оставлять окна пустыми!");
                return;
            }

            if (TbFirstName.Text == Convert.ToString(TbFirstName.Tag) || TbLastName.Text == Convert.ToString(TbLastName.Tag) || TbPatronymic.Text == Convert.ToString(TbPatronymic.Tag) ||
                TbPhone.Text == Convert.ToString(TbPhone.Tag) || TbNewLogin.Text == Convert.ToString(TbNewLogin.Tag) || TbNewPassword.Text == Convert.ToString(TbNewPassword.Tag))
            {
                MessageBox.Show("Нельзя оставлять окна пустыми!");
                return;
            }
            DB.Client newClient = new DB.Client();

            newClient.FirstName = TbFirstName.Text;
            newClient.LastName = TbLastName.Text;
            if (TbPatronymic.Text != string.Empty)
            {
                newClient.Patronymic = TbPatronymic.Text;
            }
            newClient.Phone = TbPhone.Text;
            newClient.GenderCode = (CmbGender.SelectedItem as DB.Gender).GenderCode;
            newClient.Login = TbNewLogin.Text;
            newClient.Password = TbNewPassword.Text;

            ClassHelper.EF.Context.Client.Add(newClient);
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Клиент был успешно добавлен!");

            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
