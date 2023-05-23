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

using Kingsman.ClassHelper;
using Microsoft.Win32;

namespace Kingsman.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        private string pathImage = null;
        public AddServiceWindow()
        {
            InitializeComponent();

            CmbTypeService.ItemsSource = ClassHelper.EF.Context.ServiceType.ToList();
            CmbTypeService.DisplayMemberPath = "TypeName";
            CmbTypeService.SelectedIndex = 0;

        }
        private void BtnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgImageService.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathImage = openFileDialog.FileName;
            }
            BtnChooseImage.Width = BtnChooseImage.Width;
            BtnChooseImage.Height = BtnChooseImage.Height;
        }


        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbNameService.Text) || string.IsNullOrEmpty(TbDiscService.Text) || string.IsNullOrEmpty(TbCostService.Text))
            {
                MessageBox.Show("Нет");
                return;
            }

            if (TbNameService.Text == Convert.ToString(TbNameService.Tag) || TbDiscService.Text == Convert.ToString(TbDiscService.Tag) ||
                TbCostService.Text == Convert.ToString(TbCostService.Tag))
            {
                MessageBox.Show("Нет");
                return;
            }

            if (pathImage == null)
            {
                MessageBox.Show("Нет");
                return;
            }

            //валидация 

            // добавление услуги
            DB.Service newService = new DB.Service();

            newService.Cost = Convert.ToDecimal(TbCostService.Text);
            newService.Name = TbNameService.Text;
            newService.Description = TbDiscService.Text;
            newService.ServiceTypeID = (CmbTypeService.SelectedItem as DB.ServiceType).ID;
            //if (pathImage !=  null)
            //{
            //    newService.Image = pathImage;
            //}
            try
            {
                Convert.ToDecimal(TbCostService.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Нет");
                return;
            }
            newService.ServiceTypeID = (CmbTypeService.SelectedItem as DB.ServiceType).ID;

            ClassHelper.EF.Context.Service.Add(newService);
            ClassHelper.EF.Context.SaveChanges();


            MessageBox.Show("Услуга добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
