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
using ITA.Objects;
namespace ITA
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

        List<ITA.Objects.App> selected = new List<ITA.Objects.App>();

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid item = (Grid)sender;
            CheckBox ch = ((CheckBox)item.Children[2]);
            ch.IsChecked = !ch.IsChecked;
            if (ch.IsChecked==true)
            {
                selected.Add((ITA.Objects.App)item.Tag);
            }
            else
            {
                selected.Remove((ITA.Objects.App)item.Tag);
            }
        }

        Objects.ActionQueue aq = new ActionQueue(); 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Chooser.Visibility = System.Windows.Visibility.Collapsed;
            List<InstallingApp> queue = new List<InstallingApp>();
            foreach (ITA.Objects.App sApp in selected)
            {
                InstallingApp iApp = new InstallingApp() { Origin = sApp };
                InstallItems.Items.Add(iApp);
                iApp.Downloaded += () =>
                {
                    aq.Add(() =>
                    {
                        iApp.Install();
                    });
                };
                iApp.Installed += () =>
                {
                    aq.Done();
                };
                queue.Add(iApp);
            }
            Installer.Visibility = System.Windows.Visibility.Visible;
            foreach (InstallingApp ip in queue)
                ip.Start();
        }

        private void OnHelpClick(object sender, RoutedEventArgs e)
        {
            About ab = new About();
            ab.Show();
        }
    }
}
