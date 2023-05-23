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

using Kingsman.ClassHelper;

namespace Kingsman.Windows
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
            GetListServise();
        }
        private void GetListServise()
        {
            ObservableCollection<DB.Service> listCart = new ObservableCollection<DB.Service>(ClassHelper.CartServiceClass.ServiceCart);
            LvCartService.ItemsSource = listCart;
            decimal totalSum = 0;
            foreach (var item in ClassHelper.CartServiceClass.ServiceCart)
            {
                totalSum += item.Cost * item.Quantity;
            }
            TbTotalSum.Text = Convert.ToString(totalSum) + " ₽";
        }

        private void BtnRomoveToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DB.Service; // получаем выбранную запись

            ClassHelper.CartServiceClass.ServiceCart.Remove(service);

            GetListServise();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void BtnPay_Click(object sender, RoutedEventArgs e)
        //{
        //    // покупка
        //    EF.Context.Order.Add(new DB.Order
        //    {
        //        ClientID = 1,
        //        EmployeeID = UserDataClass.Employee.ID,
        //        DateTime = DateTime.Now,
        //    }
        //    );

        //    foreach (var item in ClassHelper.CartServiceClass.ServiceCart)
        //    {
        //        DB.OrderService orderService = new DB.OrderService();
        //        orderService.OrderID = 1;
        //        orderService.ServiceID = item.ID;
        //        orderService.Quantity = 1;

        //        EF.Context.OrderService.Add(orderService);

        //    }



        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
                if (ClassHelper.CartServiceClass.ServiceCart.Count != 0)
                {
                    DB.Order newOrder = new DB.Order();

                    if (ClassHelper.UserDataClass.Employee != null)
                    {
                        newOrder.ClientID = 25;
                        newOrder.EmployeeID = ClassHelper.UserDataClass.Employee.ID;
                    }
                    else
                    {
                        newOrder.ClientID = 25;
                        newOrder.EmployeeID = 1;
                    }
                    newOrder.DateTime = DateTime.Now;

                    ClassHelper.EF.Context.Order.Add(newOrder);
                    ClassHelper.EF.Context.SaveChanges();

                    foreach (DB.Service item in ClassHelper.CartServiceClass.ServiceCart.Distinct())
                    {
                        DB.OrderService newOrderService = new DB.OrderService();
                        newOrderService.OrderID = newOrder.ID;
                        newOrderService.ServiceID = item.ID;
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

        


            //EF.Context.SaveChanges();
            // переход на главную

            this.Close();
        }
        
    }
}
