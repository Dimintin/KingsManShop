using KingsManTest.ClassHelper;
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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();
            SetServiceList();
        }

        public void SetServiceList()
        {
            LvService.ItemsSource = ClassHelper.EF.Context.Service.ToList();
        }

        private void BtnEditService_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;

            EditServiceWindow editServiceWindow = new EditServiceWindow(service);
            this.Hide();
            editServiceWindow.ShowDialog();
            this.Show();

            SetServiceList();
        }

        private void BtnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;
            var result = MessageBox.Show($"Вы уверены что хотите удалить услугу \"{service.ServiceName}\"?", "Удаление услуги", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClassHelper.EF.Context.Service.Remove(service);
                ClassHelper.EF.Context.SaveChanges();
            }
            else
            {
                return;
            }

            SetServiceList();
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow();
            this.Hide();
            addServiceWindow.ShowDialog();
            this.Show();

            SetServiceList();
        }

        private void BtnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow();
            this.Hide();
            cartWindow.ShowDialog();
            this.Show();
        }

        private void BtnAddServiceToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;

            service.Quantity++;
            CartServiceClass.cartService.Add(service);

            MessageBox.Show($"Услуга \"{service.ServiceName}\" добавлена в корзину");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
