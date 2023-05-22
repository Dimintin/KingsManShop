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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
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
            DB.Employee newEmployee = new DB.Employee();

            newEmployee.FirstName = TbFirstName.Text;
            newEmployee.LastName = TbLastName.Text;
            if (TbPatronymic.Text != string.Empty)
            {
                newEmployee.Patronymic = TbPatronymic.Text;
            }
            newEmployee.Phone = TbPhone.Text;
            newEmployee.GenderCode = (CmbGender.SelectedItem as DB.Gender).GenderCode;
            newEmployee.Login = TbNewLogin.Text;
            newEmployee.Password = TbNewPassword.Text;

            ClassHelper.EF.Context.Employee.Add(newEmployee);
            ClassHelper.EF.Context.SaveChanges();
            MessageBox.Show("Сотрудник был успешно добавлен!");

            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
