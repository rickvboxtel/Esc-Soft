using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
[assembly: InternalsVisibleTo("EscaperoomSoftware.Tests")]
namespace EscaperoomSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        IAppCache cache = new CachingService();
        Mqtt MqttClient = new Mqtt();
        UserControlGame UCgame = new UserControlGame();
        WindowEditor wEditor = new WindowEditor();

        public MainWindow()
        {
            InitializeComponent();
            if(MqttClient.Connect("localhost", "C#client") == 0)
            {
                iconConnected.Foreground = Brushes.ForestGreen;
                labelConnected.Foreground = Brushes.ForestGreen;
                labelConnected.Text = "Online";
            }

            MqttClient.Subscribe("/new/", 0);
            MqttClient.DevicesUpdate += DevicesUpdate;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MqttClient.Disconnect();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = UCgame;
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    wEditor.Show();
                    break;
                default:
                    break;
            }
        }


        public void DevicesUpdate(object sender, EventArgs e)
        {
            Console.WriteLine("Adding in listbox!!");
            RefreshDevices();
        }

        private void RefreshDevices()
        {
            Dictionary<string, Device> devices = cache.Get<Dictionary<string, Device>>("devices");
            if (devices != null)
            {

                Dispatcher.BeginInvoke(new Action(delegate () { UCgame.ListboxDevices.Items.Clear(); }));

                foreach (KeyValuePair<string, Device> device in devices)
                {
                    Dispatcher.BeginInvoke(new Action(delegate () { UCgame.ListboxDevices.Items.Add(device.Key + " | " + device.Value.Type + " | " + device.Value.Status); }));
                }
            }
        }

    }
}
