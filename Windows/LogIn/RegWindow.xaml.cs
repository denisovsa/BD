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

namespace Kingsman.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            // валидация
            if (string.IsNullOrWhiteSpace(TbName.Text))
            {
                MessageBox.Show("Поле Фамилия не заполнено");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbEmail.Text))
            {
                MessageBox.Show("Поле Имя не заполнено");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                MessageBox.Show("Поле Имя не заполнено");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbRegLogin.Text))
            {
                MessageBox.Show("Поле Имя не заполнено");
                return;
            }

            // добавление 
            DB.Client addClient = new DB.Client();
            addClient.Login = TbRegLogin.Text;
            addClient.Password = PbRegPassword.Password;
            addClient.Phone = TbPhone.Text;
            addClient.FirstName = TbName.Text;
            addClient.LastName = TbLastName.Text;
            addClient.Email = TbEmail.Text;
            if (TbLastName.Text != string.Empty)
            {
                addClient.LastName = TbLastName.Text;
            }
            addClient.ID = (CmbGender.SelectedItem as DB.Gender).ID;

            ClassHelper.EF.Context.Client.Add(addClient);

            // сохранение
            ClassHelper.EF.Context.SaveChanges();

            MessageBox.Show("Пользователь успешно добавлен");


        }

    }  
}
