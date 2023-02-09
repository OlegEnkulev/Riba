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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Riba.Resources;

namespace Riba.Pages
{
    public partial class CreateEditPage : Page
    {
        bool mode = false;
        Product product;
        public CreateEditPage()
        {
            InitializeComponent();

            for (int i = 0; i < Core.DB.Category.Count(); i++)
            {
                CategoriesCB.Items.Add(Core.DB.Category.ToList()[i].CategoryName);
            }
            CategoriesCB.SelectedIndex = 0;
        }

        public CreateEditPage(string articul)
        {
            InitializeComponent();

            for (int i = 0; i < Core.DB.Category.Count(); i++)
            {
                CategoriesCB.Items.Add(Core.DB.Category.ToList()[i].CategoryName);
            }
            CategoriesCB.SelectedIndex = 0;

            product = Core.DB.Product.Where(p => p.ProductArticleNumber == articul).FirstOrDefault();

            ArticulBox.Text = product.ProductArticleNumber;
            NameBox.Text = product.ProductName;
            PeaceBox.Text = product.ProductPeace;
            CostBox.Text = product.ProductCost.ToString();
            ManufacturerBox.Text = product.ProductManufacturer;
            DelivererBox.Text = product.ProductDeliverer;
            CategoriesCB.SelectedItem = product.Category.CategoryName;
            CountBox.Text = product.ProductQuantityInStock.ToString();
            DescriptionBox.Text = product.ProductDescription;

            mode = true;

            CreateBTN.Content = "Изменить";
        }

        private void CreateBTN_Click(object sender, RoutedEventArgs e)
        {
            if(mode == false)
            {
                try
                {
                    Core.DB.Product.Add(new Riba.Resources.Product() { ProductArticleNumber = ArticulBox.Text, ProductName = NameBox.Text, ProductPeace = PeaceBox.Text, ProductCost = Convert.ToInt32(CostBox.Text), ProductManufacturer = ManufacturerBox.Text, ProductDeliverer = DelivererBox.Text, ProductCategory = Core.DB.Category.Where(c => c.CategoryName == CategoriesCB.Text).FirstOrDefault().CategoryID, ProductQuantityInStock = Convert.ToInt32(CountBox.Text), ProductDescription = DescriptionBox.Text });
                    Core.DB.SaveChanges();
                    Core.mainWindow.MainFrame.Navigate(new AdminPage());
                }
                catch
                {
                    MessageBox.Show("Проверьте правильность введённых данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                product.ProductArticleNumber = ArticulBox.Text;
                product.ProductName = NameBox.Text;
                product.ProductPeace = PeaceBox.Text;
                product.ProductCost = Convert.ToInt32(CostBox.Text);
                product.ProductManufacturer = ManufacturerBox.Text;
                product.ProductDeliverer = DelivererBox.Text;
                product.ProductCategory = Core.DB.Category.Where(c => c.CategoryName == CategoriesCB.Text).FirstOrDefault().CategoryID;
                product.ProductQuantityInStock = Convert.ToInt32(CountBox.Text);
                product.ProductDescription = DescriptionBox.Text;

                Core.DB.Entry(product).State = System.Data.Entity.EntityState.Modified;
                Core.DB.SaveChanges();
                Core.mainWindow.MainFrame.Navigate(new AdminPage());    
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new AdminPage());
        }
    }
}
