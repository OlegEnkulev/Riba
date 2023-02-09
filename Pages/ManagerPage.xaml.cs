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
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();

            NameLabel.Content = "Пользователь: " + Core.currentUser.UserPatronymic + " " + Core.currentUser.UserName + " " + Core.currentUser.UserSurname;

            CategoriesCB.Items.Add("Не выбрано");
            for (int i = 0; i < Core.DB.Category.Count(); i++)
            {
                CategoriesCB.Items.Add(Core.DB.Category.ToList()[i].CategoryName);
            }
            CategoriesCB.SelectedIndex = 0;

            Search();
        }

        private void Search()
        {
            List<ProductView> productViews = new List<ProductView>();
            List<Product> products = Core.DB.Product.ToList();

            for (int i = 0; i < products.Count; i++)
            {
                productViews.Add(new ProductView(products[i].ProductArticleNumber, products[i].ProductName, products[i].ProductPeace, products[i].ProductCost, Convert.ToInt32(products[i].ProductMaxDiscount), products[i].ProductManufacturer, products[i].ProductDeliverer, products[i].ProductCategory, Convert.ToInt32(products[i].ProductDiscountAmount), products[i].ProductQuantityInStock, products[i].ProductDescription, products[i].ProductPhoto));
            }

            if (CategoriesCB.SelectedIndex == 0)
            {
                if (SearchBox.Text.Count() == 0)
                {
                    ProductsLB.ItemsSource = productViews;
                }
                else
                {
                    ProductsLB.ItemsSource = productViews.Where(v => v.ProductString.Contains(SearchBox.Text)).ToList();
                }
            }
            else
            {
                if (SearchBox.Text.Count() == 0)
                {
                    ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text).ToList();
                }
                else
                {
                    ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text && v.ProductString.Contains(SearchBox.Text)).ToList();
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitSystem();
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
    }
}
