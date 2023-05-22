using KingsManTest.Windows.Client;
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

namespace KingsManTest
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
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

        private void BtnSighIn_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrEmpty(PbPassword.Text) || PbPassword.Text != Convert.ToString(PbPassword.Tag)) && (string.IsNullOrEmpty(TbLogin.Text) || TbLogin.Text != Convert.ToString(TbLogin.Tag)))
            {
                var authUser = ClassHelper.EF.Context.Employee.ToList().Where(i => i.Password == PbPassword.Text && i.Login == TbLogin.Text).FirstOrDefault();

                if (authUser != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();

                    ClassHelper.SaveUserClass.SavedEmployee = authUser;
                }
                else
                {
                    MessageBox.Show("Данные пользователя неверны или не существуют", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void TbRegister_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            this.Hide();
            registrationWindow.ShowDialog();
            this.Show();
        }
    }
}
