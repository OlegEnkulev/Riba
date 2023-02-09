using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riba.Resources;

namespace Riba
{
    public static class Core
    {
        public static MainWindow mainWindow; 
        public static Resources.RibkaEntities DB = new Resources.RibkaEntities();
        public static User currentUser;

        public static void ExitSystem()
        {
            currentUser = null;
            Core.mainWindow.MainFrame.Navigate(new Pages.MainPage());
        }
    }
}
