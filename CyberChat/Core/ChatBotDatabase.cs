using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CyberChat.Core
{
    public class ChatBotDatabase
    {
    
         private string DBConnctString = "server=localhost;database=ChatBotDB;uid=root;pwd=1234;";
       

        //Data class
        public class UrgentTask
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

        }
       

        public void TaskHandler(string Title, string Description, bool IsReminderSet)
        //is_reminder_set BOOLEAN NOT NULL,

            
        {
            string createTableSql = @"
CREATE TABLE IF NOT EXISTS tasks(
    taskid INT AUTO_INCREMENT PRIMARY KEY,
    title TEXT NOT NULL,
    description TEXT NOT NULL,
    is_reminder_set BOOLEAN NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);";

            string insertSql = @"
           INSERT INTO Tasks (Title, Description,is_reminder_set) 
          VALUES (@Title, @Description, @IsReminderSet);";
            using (MySqlConnection conn = new MySqlConnection(DBConnctString))
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand createCmd = new MySqlCommand(createTableSql, conn))
                    {
                        createCmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand inserCMD = new MySqlCommand(insertSql, conn))
                    {
                        //attribute and values
                        inserCMD.Parameters.AddWithValue("@Title", Title);
                        inserCMD.Parameters.AddWithValue("@Description", Description);
                        inserCMD.Parameters.AddWithValue("@IsReminderSet", IsReminderSet);
                        inserCMD.ExecuteNonQuery();

                        MessageBox.Show("Database initiation successful: Tasks", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error saving into database: {ex.Message}", "Database Error NewTasksTable", MessageBoxButton.OK, MessageBoxImage.Error);
                }
          
            }
        }

        public void CheckReminder()
        {

            string selectSql = "SELECT taskid, title, description FROM tasks WHERE is_reminder_set = 1 LIMIT 1;";
            string updateSql = "UPDATE tasks SET is_reminder_set = 0 WHERE taskid = @TaskId;";

            int taskId = -1;
            string title = string.Empty;
            string description = string.Empty;
            bool foundReminder = false;

            using (MySqlConnection conn = new MySqlConnection(DBConnctString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand selectCmd = new MySqlCommand(selectSql, conn))
                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            taskId = reader.GetInt32("taskid");
                            title = reader.GetString("title");
                            description = reader.GetString("description");
                            foundReminder = true;
                        }
                    }

                    if (foundReminder)
                    {
                        MessageBox.Show($"Reminder: {title}\n\n{description}", "Task Alert", MessageBoxButton.OK, MessageBoxImage.Information);

                        using (MySqlCommand updateCmd = new MySqlCommand(updateSql, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@TaskId", taskId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error executing reminder: {e.Message}");
                }
                }

            }
        //Method to extract tasklist from DB
        public List<string> GetTasks()
        {
            List<int> TaskId = new List<int>();
            List<string> tasks = new List<string>();

            List<string> description = new List<string>();

            string sqlQuery = "SELECT * FROM tasks ORDER BY timestamp DESC";

            using (MySqlConnection conn = new MySqlConnection(DBConnctString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(sqlQuery, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(reader.GetString("title"));
                        //TaskId.Add(reader.GetInt16("TaskId"));
                        //description.Add(reader.GetString("Description"));
                        
                    }
                }
            }

            return tasks;
        }

        public void DeleteTasks(int taskId)
        {

            try
            {
                string queryToDelete = "DELETE FROM tasks WHERE taskId = @taskId";
                

                using (MySqlConnection conn = new MySqlConnection(DBConnctString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(queryToDelete, conn))
                    {
                        
                        cmd.Parameters.Add("@taskId", MySqlDbType.Int32, taskId).Value = taskId;

         
                        int rowsAffected = cmd.ExecuteNonQuery();

                       
                        if (rowsAffected > 0)
                        {
                         
                            MessageBox.Show("Task deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No task found with that ID.");
                        }
                    }
             
                }
               
            }

            catch (Exception e)
            {
                MessageBox.Show("Error occurred attempting deletion: " + e.Message);
            }

            
        }

        public void ListMyDb(DataGrid DataGridTasks)
        {
            string sqlQuery = "SELECT * FROM tasks";
            {
                //1st step connection string
                using (MySqlConnection conn = new MySqlConnection (DBConnctString))
                {
                    try
                    {
                        //Step 2: connect to the server
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(sqlQuery, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DataGridTasks.ItemsSource = dt.DefaultView;

                        conn.Close();

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Database listing error" +e.Message);
                    }

                }
            }
           
        }

        public void SaveLogToDatabase(string userInput, string botResponse)
        {
            try
            {
                using (var connection = new MySqlConnection(DBConnctString))
                {
                    connection.Open();
                    string query = "INSERT INTO ActivityLogs (UserInput, BotResponse, Timestamp) VALUES (@input, @response, NOW())";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@input", userInput);
                        cmd.Parameters.AddWithValue("@response", botResponse);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Database logging failed: " + e.Message);
            }
        }

        public string GetActivityLogFromDatabase()
        {
            var logs = new System.Text.StringBuilder();
            string safeUser = ("name") ?? "User";
            logs.AppendLine($"{safeUser}, here are the recent actions recorded in the log:\n");

            try
            {
                using (var connection = new MySqlConnection(DBConnctString))
                {
                    connection.Open();
                    // Pulls the last 5 recorded actions
                    string query = "SELECT UserInput, BotResponse, Timestamp FROM ActivityLogs ORDER BY Timestamp DESC LIMIT 5";

                    using (var cmd = new MySqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows) return "No actions have been recorded in the log yet.";

                        while (reader.Read())
                        {
                            string timestamp = reader.GetDateTime("Timestamp").ToString("yyyy-MM-dd HH:mm:ss");
                            string input = reader.GetString("UserInput");
                            string response = reader.GetString("BotResponse");

                            logs.AppendLine($"[{timestamp}] User: {input} -> Bot: {response}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Database retrieval failed: " + e.Message);
                return "Sorry, I encountered an error pulling the activity logs.";
            }

            return logs.ToString();
        }

    }
    }
