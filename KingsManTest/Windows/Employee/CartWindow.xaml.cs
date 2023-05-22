using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            SetServiceList();
        }

        public void SetServiceList()
        {
            ObservableCollection<DB.Service> listCart = new ObservableCollection<DB.Service>(ClassHelper.CartServiceClass.cartService);
            LvCartService.ItemsSource = listCart.Distinct();
            decimal totalSum = 0;
            foreach (var item in ClassHelper.CartServiceClass.cartService)
            {
                totalSum += item.Price * item.Quantity;
            }
            TbTotalSum.Text = Convert.ToString(totalSum) + " ₽";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnDeleteCartService_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service; // получаем выбранную запись

            ClassHelper.CartServiceClass.cartService.Remove(service);
            SetServiceList();

            service.Quantity = 0;
        }

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ClassHelper.CartServiceClass.cartService.Count != 0)
            {
                DB.Order newOrder = new DB.Order();

                if (ClassHelper.SaveUserClass.SavedEmployee != null)
                {
                    newOrder.ClientId = 25;
                    newOrder.EmployeeId = ClassHelper.SaveUserClass.SavedEmployee.Id;
                }
                else
                {
                    newOrder.ClientId = 25;
                    newOrder.EmployeeId = 1;
                }
                newOrder.OrderDate = DateTime.Now;

                ClassHelper.EF.Context.Order.Add(newOrder);
                ClassHelper.EF.Context.SaveChanges();

                foreach (DB.Service item in ClassHelper.CartServiceClass.cartService.Distinct())
                {
                    DB.OrderService newOrderService = new DB.OrderService();
                    newOrderService.OrderId = newOrder.Id;
                    newOrderService.ServiceId = item.Id;
                    newOrderService.Quantity = item.Quantity;

                    ClassHelper.EF.Context.OrderService.Add(newOrderService);
                    ClassHelper.EF.Context.SaveChanges();
                }

                MessageBox.Show("Заказ успешно оформлен!");
            }
            else
            {
                MessageBox.Show("Корзина пуста");
            }

            this.Close();
            
        }

        private void BtnCartLessQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;

            if (service.Quantity > 1)
            {
                service.Quantity--;
            }

            SetServiceList();
        }

        private void BtnCartMoreQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service;

            if (service.Quantity < 10)
            {
                service.Quantity++;
            }

            SetServiceList();
        }
    }
}
