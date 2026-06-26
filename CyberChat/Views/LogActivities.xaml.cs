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

        private void LoadActivityLogs()
        {
            
            string completeLogText = _db.GetActivityLogFromDatabase();

            
            string[] logLines = completeLogText.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            
            LogArea.ItemsSource = logLines;
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.ChatInterfaceGrid.Visibility = Visibility.Visible;
                mainWindow.SubWindowContainer.Visibility = Visibility.Collapsed;
                mainWindow.SubWindowContainer.Content = null;
            }
        }
    }
}
