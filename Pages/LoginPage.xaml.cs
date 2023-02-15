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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBTN_Click(object sender, RoutedEventArgs e)
        {
            if(LoginBox.Text.Count() < 3)
            {
                Mistake();
                return;
            }
            if(PassBox.Password.Count() < 3)
            {
                Mistake();
                return;
            }

            User user = Core.DB.User.Where(u => u.UserLogin == LoginBox.Text && u.UserPassword == PassBox.Password).FirstOrDefault();

            if(user == null)
            {
                Mistake();
                return;
            }

            Core.currentUser = user;

            switch (user.UserRole)
            {
                case 1:
                    Core.mainWindow.MainFrame.Navigate(new Pages.AdminPage());
                    break;
                case 2:
                    Core.mainWindow.MainFrame.Navigate(new Pages.ManagerPage());
                    break;
                case 3:
                    Core.mainWindow.MainFrame.Navigate(new Pages.UserPage());
                    break;
                default:
                    Mistake();
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Core.ExitSystem();
        }


        private void Mistake()
        {
            MessageBox.Show("Проверьте правильность введённых данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            Windows.CapchaWindow capchaWindow = new Windows.CapchaWindow();
            capchaWindow.Show();
        }
    }
}
