using Chatty;
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
using System.Windows.Shell;

using static CyberChat.SentimentDetector;

namespace CyberChat
{
    partial class TaskScheduler : Window
    {

        private ChatBot bot;
        private readonly ChatBotDatabase db = new ChatBotDatabase();
        public TaskScheduler()
        {
            InitializeComponent();



        }



        private void SetReminderBtn_Click(object sender, RoutedEventArgs e)
        {

            string title = TitleBox.Text;
            string description = DescriptionBox.Text;
            bool reminder = reminderBox.IsChecked == true;
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title for your task.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            db.TaskHandler(title, description, reminder);

            ClearInputFields();

        }
     
        private string AddTitle()
        {
            
           string Title = TitleBox.Text;
            if (string.IsNullOrEmpty(Title))
            {
                MessageBox.Show("Enter title");
                return null;
            }

            return Title;
        }
        private void AddDescription()
        {
         
        }

        //click method save
        private void SafeTask(object sender, RoutedEventArgs e)
        {

            string title = TitleBox.Text;
            string description = DescriptionBox.Text;
            bool reminder = reminderBox.IsChecked == true;

            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Enter title and descriptions");
            }

            //storing details into the databases
            db.TaskHandler(title, description, reminder);

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        public string ClearInputFields()
        {
            return "all fields have been stored on the data";
        }
    }
}
