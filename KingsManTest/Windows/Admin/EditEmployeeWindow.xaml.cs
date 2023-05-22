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
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {

        DB.Employee addedEmployee = null;
        public EditEmployeeWindow()
        {
            InitializeComponent();
        }
        public EditEmployeeWindow(DB.Employee employee)
        {
            InitializeComponent();

            CmbGender.ItemsSource = ClassHelper.EF.Context.Gender.ToList();
            CmbStaffPosition.ItemsSource = ClassHelper.EF.Context.Position.ToList();
            CmbGender.DisplayMemberPath = "GenderName";
            CmbStaffPosition.DisplayMemberPath = "PositionName";

            TbFirstName.Text = employee.FirstName;
            TbLastName.Text = employee.LastName;
            TbPatronymic.Text = employee.Patronymic;
            TbNewLogin.Text = employee.Login;
            TbNewPassword.Text = employee.Password;
            TbPhone.Text = employee.Phone;
            CmbStaffPosition.SelectedItem = ClassHelper.EF.Context.Position.ToList().Where(i => i.Id == employee.PositionId).FirstOrDefault();
            CmbGender.SelectedItem = ClassHelper.EF.Context.Gender.ToList().Where(i => i.GenderCode == employee.GenderCode).FirstOrDefault();

            addedEmployee = employee;
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
            addedEmployee.FirstName = TbFirstName.Text;
            addedEmployee.LastName = TbLastName.Text;
            addedEmployee.Patronymic = TbPatronymic.Text;
            addedEmployee.Phone = TbPhone.Text;
            addedEmployee.Login = TbNewLogin.Text;
            addedEmployee.Password = TbNewPassword.Text;
            addedEmployee.PositionId = (CmbStaffPosition.SelectedItem as DB.Position).Id;
            addedEmployee.GenderCode = (CmbGender.SelectedItem as DB.Gender).GenderCode;

            ClassHelper.EF.Context.Employee.Add(addedEmployee);
          

            MessageBox.Show("OK");
            this.Close();
        }

        private void TbBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
