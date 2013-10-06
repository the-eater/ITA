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
using ITSA.Objects;
namespace ITSA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        List<ITSA.Objects.App> selected = new List<ITSA.Objects.App>();

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid item = (Grid)sender;
            CheckBox ch = ((CheckBox)item.Children[2]);
            ch.IsChecked = !ch.IsChecked;
            if (ch.IsChecked==true)
            {
                selected.Add((ITSA.Objects.App)item.Tag);
            }
            else
            {
                selected.Remove((ITSA.Objects.App)item.Tag);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Chooser.Visibility = System.Windows.Visibility.Collapsed;
            List<InstallingApp> queue = new List<InstallingApp>();
            foreach (ITSA.Objects.App sApp in selected)
            {
                InstallingApp iApp = new InstallingApp() { Origin = sApp };
                InstallItems.Items.Add(iApp);
                queue.Add(iApp);
            }
            foreach (InstallingApp ip in queue)
                ip.Start();
            Installer.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
