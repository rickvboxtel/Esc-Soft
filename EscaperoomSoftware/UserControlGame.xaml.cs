using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Blockly;
using Blockly.Blocks;
using System.IO;
using LazyCache;
using System.ComponentModel;

namespace EscaperoomSoftware
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlGame : UserControl, IDisposable
    {
        IAppCache Cache = new CachingService();
        Mqtt MqttClient = new Mqtt();

        private readonly BackgroundWorker bgwStartWorker = new BackgroundWorker();
        private readonly BackgroundWorker bgwBackgroundWorker = new BackgroundWorker();

        public UserControlGame()
        {
            InitializeComponent();
            //Subscribe to events
            
            bgwStartWorker.DoWork += BgwStartWorker_DoWork;
            bgwBackgroundWorker.DoWork += BgwBackgroundWorker_DoWork;
        }

        private void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked");
            RefreshDevices(); // Make sure ALL the devices have been discovered.
            bgwStartWorker.RunWorkerAsync();
            bgwBackgroundWorker.RunWorkerAsync();
        }

        private void ButtonStopGame_Click(object sender, RoutedEventArgs e)
        {
            Cache.Add("stop", true);
        }

        private void ButtonResetGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRefreshDevices_Click(object sender, RoutedEventArgs e)
        {
            RefreshDevices();
        }

        private void ButtonGetDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            if(ListboxDevices.SelectedItem != null)
            {
                Device device = Cache.Get<Dictionary<string, Device>>("devices")[ListboxDevices.SelectedItem.ToString().Split(' ')[0]];
                string message = "MAC Address: " + device.Mac + Environment.NewLine + "Type: " + device.Type + Environment.NewLine
                    + "Status:" + device.Status + Environment.NewLine + "Error: " + device.Error + Environment.NewLine
                    + Environment.NewLine + "TypeSpecific: " + Environment.NewLine;

                foreach (KeyValuePair<string, object> type in device.Misc)
                {
                    message += type.Key + ": " + type.Value + Environment.NewLine;
                }

                MessageBox.Show(message);
            }
        }

        private void BgwStartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("in dowork");
            Cache.Add("stop", false);
            Parser parser = new Parser();
            parser.AddStandardBlocks();
            Console.WriteLine("in dowork2");
            var workspace = parser.Parse(File.ReadAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\GameFiles\gameFile.xml"));
            //var workspace = parser.Parse(File.ReadAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\MOSQUITTO\UITest\UITest\bin\Debug\gameFile.xml"));
            Console.WriteLine("in dowork3");
            var output = workspace.Evaluate();
            MessageBox.Show("done");
        }

        private void BgwBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            /*cache.Add("stop", false);
            Parser parser = new Parser();
            parser.AddStandardBlocks();
            var workspace = parser.Parse(File.ReadAllText(@"C:\Users\rick_\Desktop\WFtest\BlocklyTest\test.xml"));
            var output = workspace.Evaluate();
            MessageBox.Show("done");*/
        }

        private void RefreshDevices()
        {
            MqttClient.Publish("/allClients/", "getInfo");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            bgwBackgroundWorker.Dispose();
            bgwStartWorker.Dispose();
        }
    }
}
