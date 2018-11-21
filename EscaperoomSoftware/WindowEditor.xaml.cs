using System;
using System.Windows;

namespace EscaperoomSoftware
{
    /// <summary>
    /// Interaction logic for WindowEditor.xaml
    /// </summary>
    public partial class WindowEditor : Window
    {

        private static string folder = @"C:\Users\rick_\Dropbox\ICT\Afstuderen\SCHOOL\4. Implementatie\EscaperoomSoftware\EscaperoomSoftware\Files\";
        private static string gameFile = folder + @"GameFiles\gameFile.xml";
        private static string backgroundFile = folder + @"GameFiles\backgroundFile.xml";
        private static string blocklyFile = folder + @"BlocklyBrowserFiles\blocklyHTML.html";
        private static string blocklyToolboxFile = folder + @"BlocklyBrowserFiles\blocklyToolbox.xml";

        public WindowEditor()
        {
            InitializeComponent();
            LabelState.Content = "Editor";
        }

        private void Browser_GameLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadFile(gameFile);
        }
        private void Browser_BackgroundLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadFile(backgroundFile);
        }

        private void GameFileButton_Click(object sender, RoutedEventArgs e)
        {
            LabelLastSaved.Content = "";
            GamefileButton.IsEnabled = false;
            Browser.LoadCompleted += Browser_GameLoadCompleted;
            Browser.LoadCompleted -= Browser_BackgroundLoadCompleted;
            Browser.NavigateToString(System.IO.File.ReadAllText(blocklyFile));
            LabelState.Content = "Gamefile Editor";
            BackgroundfileButton.IsEnabled = true;
        }

        private void BackgroundFileButton_Click(object sender, RoutedEventArgs e)
        {
            LabelLastSaved.Content = "";
            BackgroundfileButton.IsEnabled = false;
            Browser.LoadCompleted -= Browser_GameLoadCompleted;
            Browser.LoadCompleted += Browser_BackgroundLoadCompleted;
            Browser.NavigateToString(System.IO.File.ReadAllText(blocklyFile));
            LabelState.Content = "Backgroundfile Editor";
            GamefileButton.IsEnabled = true;
            

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = false;
            string result = (string)Browser.InvokeScript("showXML", new object[] { });
            if(GamefileButton.IsEnabled)
            {
                System.IO.File.WriteAllText(backgroundFile, result);
            }
            else if(BackgroundfileButton.IsEnabled)
            {
                System.IO.File.WriteAllText(gameFile, result);
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
            var toolboxXML = System.IO.File.ReadAllText(blocklyToolboxFile);
            var workspaceXML = System.IO.File.ReadAllText(completeFilePath);
            //Initialize blocky using toolbox and workspace
            Browser.InvokeScript("init", new object[] { toolboxXML, workspaceXML });

        }
    }
}
