
using CyberChat.Core;
using CyberChat.Views;
using MySql.Data.MySqlClient;
using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using static CyberChat.Core.SentimentDetector;

namespace CyberChat
{
    partial class TaskScheduler : Window
    {
        public ObservableCollection<string> Tasks { get; set; } = new ObservableCollection<string>();
        private ChatBot bot;
        
        private DispatcherTimer reminderTimer;
        private readonly ChatBotDatabase db = new ChatBotDatabase();
        public TaskScheduler()
        {
            InitializeComponent();
            this.DataContext = this;


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

                Tasks.Clear();

                foreach (string task in db.GetTasks())
                {
                    Tasks.Add(task);
                   
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
        private void deleteContxtMenu(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem == null) return;
            //get contextMenu
            ContextMenu contextMenu = menuItem.Parent as ContextMenu;
            if (contextMenu == null) return;
            DataGrid dataGrid = contextMenu.PlacementTarget as DataGrid;
            if (dataGrid == null || dataGrid.SelectedItem == null) return;
            DataRowView row = dataGrid.SelectedItem as DataRowView;
            if (row == null) return;
            string taskid = row["id"].ToString();
            //confirm dialogbox
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
            if(res == MessageBoxResult.Yes)
            {
                if (int.TryParse(taskid, out int idToDelete))
                {
                 db.DeleteTasks(idToDelete);
                    db.ListMyDb(datagridtasks);
                }
                else
                {
                    MessageBox.Show("Invalid Task ID format.");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //db.ListMyDb();
            db.ListMyDb(datagridtasks);
        }
       
       
    }
}
