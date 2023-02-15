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
using System.Windows.Threading;

namespace Riba.Windows
{
    public partial class CapchaWindow : Window
    {
        int timerSeconds = 10;
        public CapchaWindow()
        {
            InitializeComponent();

            Core.mainWindow.IsEnabled = false;

            SetTimer();
        }

        private void SubmitBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CapchaBox.Text == "BU34S")
            {
                Passed();
            }
            else
            {
                timerSeconds = 10;
            }
        }

        void SetTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timerSeconds > 0)
            {
                SubmitBTN.IsEnabled = false;
                SubmitBTN.Content = timerSeconds.ToString();
                timerSeconds--;
            }
            else
            {
                SubmitBTN.IsEnabled = true;
                SubmitBTN.Content = "Подтвердить";
            }
        }

        void Passed()
        {
            Core.mainWindow.IsEnabled = true;
            this.Close();
        }
    }
}
