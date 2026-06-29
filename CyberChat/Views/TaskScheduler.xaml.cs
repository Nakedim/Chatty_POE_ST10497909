using CyberChat.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
namespace CyberChat
{
    public partial class TaskScheduler : Window
    {
        private bool _isReminderSet = false;
        ChatBotDatabase db = new ChatBotDatabase();
        public TaskScheduler()
        {
            InitializeComponent();
            InitializeTimeSelectorItems();

        }

        private void InitializeTimeSelectorItems()
        {
            // Populate matching 24 hour standard range loop indices
            for (int h = 0; h < 24; h++) ComboHours.Items.Add(h.ToString("D2"));
            for (int m = 0; m < 60; m++) ComboMinutes.Items.Add(m.ToString("D2"));

            // Pre-select current timestamp metrics context values automatically
            TargetDatePicker.SelectedDate = DateTime.Now;
            ComboHours.SelectedItem = DateTime.Now.Hour.ToString("D2");
            ComboMinutes.SelectedItem = DateTime.Now.Minute.ToString("D2");
        }


        
        private void SetReminderButton_Click(object sender, RoutedEventArgs e)
        {
            _isReminderSet =!_isReminderSet;

            if (_isReminderSet)
            {
                btnSetReminder.Content = "🔔 Reminder Set Active";
            }
        string title = TitleBox.Text.Trim();
            string description = DescriptionBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please provide both a Title and Description for your task.", "Inputs Missing");
                return;
            }

            if (TargetDatePicker.SelectedDate == null || ComboHours.SelectedItem == null || ComboMinutes.SelectedItem == null)
            {
                MessageBox.Show("Please configure a valid complete future Date and Time constraint.", "Time Validation Error");
                return;
            }

            // Combine form inputs safely into a distinct target date timestamp
            DateTime date = TargetDatePicker.SelectedDate.Value;
            int hrs = int.Parse(ComboHours.SelectedItem.ToString());
            int mins = int.Parse(ComboMinutes.SelectedItem.ToString());
            DateTime targetReminderTime = new DateTime(date.Year, date.Month, date.Day, hrs, mins, 0);

            TimeSpan durationRemaining = targetReminderTime - DateTime.Now;

            if (durationRemaining.TotalMilliseconds <= 0)
            {
                MessageBox.Show("The chosen target reminder timestamp has already passed. Please select a point in the future.", "Scheduling Conflict");
                //CyberChat.Core.AppStateManager.TrackAction($"ReminderNortification - {} sen."
                return;
            }

            MessageBox.Show($"Notification confirmed for {targetReminderTime.ToString("g")}!", "Reminder Armed");

            // Spin a non-blocking background asynchronous execution task path
            Task.Run(async () =>
            {
                await Task.Delay(durationRemaining);

               
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(
                        $"🔔 CURRENT SYSTEM TASK ALERT:\n\n📌 Title: {title}\n📝 Details: {description}",
                        "CyberChat Security System Alert Notification",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation
                    );
                });
            });
        }

        private void SafeTask(object sender, RoutedEventArgs e)
        {
            string userTitle = TitleBox.Text;
            string userDescription = DescriptionBox.Text;
            
            db.TaskHandler("New Task Title", "Description", _isReminderSet);

            MessageBox.Show("Task logged to workspace ledger storage successfully.", "Task Saved");

            TitleBox.Clear();
            DescriptionBox.Clear();
            _isReminderSet = false;
            btnSetReminder.Content = "Set Reminder";
            RefreshTaskViews();
        }

        private void ViewTasksButton_Click(object sender, RoutedEventArgs e)
        { 
            db.ListMyDb(datagridtasks);
          
        }

        private void deleteContxtMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Prompt user with a confirmation safety dialogue
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to permanently delete this task?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result != MessageBoxResult.Yes) return;

                int taskIdToDelete = -1;

                // 2. Scenario A: Item deleted via the DataGrid Context Menu
                if (datagridtasks.SelectedItem != null)
                {
                    
                    dynamic selectedRow = datagridtasks.SelectedItem;
                    taskIdToDelete = selectedRow.ID;
                }
                // 3. Scenario B: Item deleted via the ListBox Context Menu
                else if (listBoxArea.SelectedItem != null)
                {
                    dynamic selectedItem = listBoxArea.SelectedItem;
                    taskIdToDelete = selectedItem.ID;
                }
                else
                {
                    MessageBox.Show("Please select an item to delete first.", "Selection Error");
                    return;
                }

                // 4. Execute the deletion query using Database component layer
                if (taskIdToDelete != -1)
                {
                    // Instantiate project database helper class
                    ChatBotDatabase db = new ChatBotDatabase();

                    // Call database execution delete command logic query method
                    bool isDeleted = db.DeleteTasks(taskIdToDelete);
                    
                    if (isDeleted)
                    {
                        MessageBox.Show("Task deleted successfully from storage.", "Success");

                        // 5. Force the grids to refresh live to reflect changes instantly
                        RefreshTaskViews();
                        ;
                    }
                    else
                    {
                        MessageBox.Show("Could not process deletion request in the database backend.", "Database Error");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An explicit system error occurred during execution: {ex.Message}", "System Error Exception");
            }
        }
        private void RefreshTaskViews()
        {
            datagridtasks.SelectedItem = null;
            listBoxArea.SelectedItem = null;


            ViewTasksButton_Click(this, null);
        }
       
    }
}