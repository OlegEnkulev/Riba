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
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();

            NameLabel.Content = "Пользователь: " + Core.currentUser.UserPatronymic + " " + Core.currentUser.UserName + " " + Core.currentUser.UserSurname;

            CategoriesCB.Items.Add("Не выбрано");
            for (int i = 0; i < Core.DB.Category.Count(); i++)
            {
                CategoriesCB.Items.Add(Core.DB.Category.ToList()[i].CategoryName);
            }
            CategoriesCB.SelectedIndex = 0;
            SortCB.SelectedIndex = 0;

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

            switch (SortCB.SelectedIndex)
            {
                case 0:
                    if (CategoriesCB.SelectedIndex == 0)
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.OrderBy(o => o.Name);
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.ProductString.Contains(SearchBox.Text)).OrderBy(o => o.Name).ToList();
                        }
                    }
                    else
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text).OrderBy(o => o.Name).ToList();
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text && v.ProductString.Contains(SearchBox.Text)).OrderBy(o => o.Name).ToList();
                        }
                    }
                    break;
                case 1:
                    if (CategoriesCB.SelectedIndex == 0)
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.OrderBy(o => o.Cost);
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.ProductString.Contains(SearchBox.Text)).OrderBy(o => o.Cost).ToList();
                        }
                    }
                    else
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text).OrderBy(o => o.Cost).ToList();
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text && v.ProductString.Contains(SearchBox.Text)).OrderBy(o => o.Cost).ToList();
                        }
                    }
                    break;
                case 2:
                    if (CategoriesCB.SelectedIndex == 0)
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.OrderByDescending(o => o.Cost);
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.ProductString.Contains(SearchBox.Text)).OrderByDescending(o => o.Cost).ToList();
                        }
                    }
                    else
                    {
                        if (SearchBox.Text.Count() == 0)
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text).OrderByDescending(o => o.Cost).ToList();
                        }
                        else
                        {
                            ProductsLB.ItemsSource = productViews.Where(v => v.CategoryName == CategoriesCB.Text && v.ProductString.Contains(SearchBox.Text)).OrderByDescending(o => o.Cost).ToList();
                        }
                    }
                    break;
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

        private void CreateBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.mainWindow.MainFrame.Navigate(new CreateEditPage());
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsLB.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                ProductView product = (ProductView)ProductsLB.SelectedItem;
                Core.mainWindow.MainFrame.Navigate(new CreateEditPage(product.Article));
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if(ProductsLB.SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(MessageBox.Show("Вы уверены, что хотите удалить это?", "Проверка", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
            {
                try
                {
                    ProductView product = (ProductView)ProductsLB.SelectedItem;
                    Product delProduct = Core.DB.Product.Where(p => p.ProductArticleNumber == product.Article).FirstOrDefault();
                    Core.DB.Product.Remove(delProduct);
                    Core.DB.SaveChanges();
                    Search();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}
