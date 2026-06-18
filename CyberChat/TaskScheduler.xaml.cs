using Chatty;
using MySql.Data.MySqlClient;
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
using System.Windows.Threading;
using static CyberChat.SentimentDetector;

namespace CyberChat
{
    partial class TaskScheduler : Window
    {

        private ChatBot bot;
        private DispatcherTimer reminderTimer;
        private readonly ChatBotDatabase db = new ChatBotDatabase();
        public TaskScheduler()
        {
            InitializeComponent();
            //SetReminderNortification();

        }

        public void SetReminderTimer()
        {
            reminderTimer = new DispatcherTimer();
            reminderTimer.Interval = TimeSpan.FromSeconds(30);
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private void ReminderTimer_Tick(object? sender, EventArgs e)
        {
            //CheckReminder();
        }


        private void SetReminderBtn_Click(object sender, RoutedEventArgs e)
        {

            string title = TitleBox.Text;
            string description = DescriptionBox.Text;
            //bool reminder = reminderBox.IsChecked == true;
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a title for your task.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            db.TaskHandler(title, description,true);

           

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
 
        //click method save
        private void SafeTask(object sender, RoutedEventArgs e)
        {

            string title = TitleBox.Text;
            string description = DescriptionBox.Text;
            //bool reminder = reminderBox.IsChecked == true;

            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Enter title and descriptions");
            }
            //storing details into the databases
            db.TaskHandler(title, description,true);

            MessageBoxResult res = MessageBox.Show("Do you also want to set a reminder", "Reminder", 
                MessageBoxButton.YesNo,MessageBoxImage.Question, MessageBoxResult.Yes);

            if (res == MessageBoxResult.Yes)
            {
                //pop up a calender to set reminder
                MessageBox.Show("Reminder have been set");
            }

            

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxArea.Items.Clear();

                foreach (string task in db.GetTasks())
                {
                    ListBoxArea.Items.Add(task);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}");
            }
        }

        private void DeleteTasks(object sender, RoutedEventArgs e)
        {
            db.DeleteTasks(1);
        }
    }
}
