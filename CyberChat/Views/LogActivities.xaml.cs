using System;
using System.Windows;
using System.Windows.Controls;
using CyberChat.Core; 

namespace CyberChat.Views
{
    public partial class LogActivities : Page
    {
        
        private readonly ChatBotDatabase _db = new ChatBotDatabase();

        

        public LogActivities()
        {
            InitializeComponent();

            // Run the load method when the page initializes
            LoadActivityLogs();
        }
        public class ActivityLogItem
        {
            public string Timestamp { get; set; }
            public string Action { get; set; }
            public string Details { get; set; }
        }
        private void LoadActivityLogs()
        {
            List<string> Applogs = FetchLogFromSource();
            List<string> formattedAppLogs = new List<string>();

            LogArea.Items.Clear();

            for(int i =0; i< Applogs.Count; i++)
            {   
                int itemNumber = i + 1;
                formattedAppLogs.Add($"{i + 1}. {Applogs[i]}");
               
            }
          LogArea_AppActions.ItemsSource = formattedAppLogs;

            if (formattedAppLogs.Count >0)
            {
                LogArea_AppActions.ScrollIntoView(formattedAppLogs[formattedAppLogs.Count - 1]);
            }
            string completeLogText = _db.GetActivityLogFromDatabase();
            string[] logLines = completeLogText.Split(new[] { Environment.NewLine, "\n" },
                StringSplitOptions.RemoveEmptyEntries);
            LogArea.ItemsSource = logLines;
            if(logLines.Length > 0) {
                LogArea.ScrollIntoView(logLines[logLines.Length - 1]);
            } 
        }

        private List<string> FetchLogFromSource()
        {
            return CyberChat.Core.AppStateManager.ActivityLog;

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.ChatInterfaceGrid.Visibility = Visibility.Visible;
                mainWindow.SubWindowContainer.Visibility = Visibility.Collapsed;
                mainWindow.SubWindowContainer.Content = null;
            }

            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                                this.NavigationService.GoBack();
            }
        }
    }
}
