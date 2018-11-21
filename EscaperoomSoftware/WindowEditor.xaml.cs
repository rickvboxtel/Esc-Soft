using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EscaperoomSoftware
{
    /// <summary>
    /// Interaction logic for WindowEditor.xaml
    /// </summary>
    public partial class WindowEditor : Window
    {
        public WindowEditor()
        {
            InitializeComponent();
            LabelState.Content = "Editor";
            

        }

        private void Browser_GameLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadFile(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\GameFiles\gameFile.xml");
        }
        private void Browser_BackgroundLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadFile(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\GameFiles\backgroundFile.xml");
        }

        private void GameFileButton_Click(object sender, RoutedEventArgs e)
        {
            LabelLastSaved.Content = "";
            GamefileButton.IsEnabled = false;
            Browser.LoadCompleted += Browser_GameLoadCompleted;
            Browser.LoadCompleted -= Browser_BackgroundLoadCompleted;
            Browser.NavigateToString(System.IO.File.ReadAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\BlocklyBrowserFiles\blocklyHTML.html"));
            LabelState.Content = "Gamefile Editor";
            BackgroundfileButton.IsEnabled = true;
        }

        private void BackgroundFileButton_Click(object sender, RoutedEventArgs e)
        {
            LabelLastSaved.Content = "";
            BackgroundfileButton.IsEnabled = false;
            Browser.LoadCompleted -= Browser_GameLoadCompleted;
            Browser.LoadCompleted += Browser_BackgroundLoadCompleted;
            Browser.NavigateToString(System.IO.File.ReadAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\BlocklyBrowserFiles\blocklyHTML.html"));
            LabelState.Content = "Backgroundfile Editor";
            GamefileButton.IsEnabled = true;
            

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            string result = (string)Browser.InvokeScript("showXML", new object[] { });
            if(GamefileButton.IsEnabled)
            {
                System.IO.File.WriteAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\GameFiles\backgroundFile.xml", result);
            }
            else if(BackgroundfileButton.IsEnabled)
            {
                System.IO.File.WriteAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\GameFiles\gameFile.xml", result);
            }
            SaveButton.IsEnabled = true;
            LabelLastSaved.Content = "Last saved: " + DateTime.Now.ToString("HH:mm:ss tt");
        }


        private void LoadFile(string completeFilePath)
        {
            if (completeFilePath == null)
            {
                throw new ArgumentNullException(nameof(completeFilePath));
            }
            //ShowCodeButton.IsEnabled = true;
            var toolboxXML = System.IO.File.ReadAllText(@"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\BlocklyBrowserFiles\blocklyToolbox.xml");
            var workspaceXML = System.IO.File.ReadAllText(completeFilePath);
            //Initialize blocky using toolbox and workspace
            Browser.InvokeScript("init", new object[] { toolboxXML, workspaceXML });

        }
    }
}
