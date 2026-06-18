using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CyberChat
{
    public class ChatBotDatabase
    {
    
         private string DBConnctString = "server=localhost;database=ChatBotDB;uid=root;pwd=Nakedim@dac702;";
       

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
           INSERT INTO Tasks (title, description,is_reminder_set) 
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

            string selectSql = "Select taskid, title, description " +
                "FROM task WHERE is_reminder_set = 1 LIMIT 1;";
            String updateSql = "UPDATE tasks SET is_reminder_set = 0 WHERE taskid = @TaskId;";
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
                    {
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
                    }
                    if (foundReminder)
                    {
                        //display popup
                     MessageBox.Show($"Reminder:{title}\n\n{description}","Task Alert",
                      MessageBoxButton.OK,
                         MessageBoxImage.Information);
                        using (MySqlCommand updateCmd = new MySqlCommand(updateSql, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@TaskId", taskId);
                            updateCmd.ExecuteNonQuery();
                        }
                     }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"error executing reminder");
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
                        description.Add(reader.GetString("Description"));
                        TaskId.Add(reader.GetInt16("TaskId"));
                    }
                }
            }

            return tasks;
        }

        public void DeleteTasks()
        {
            try
            {
                string queryToDelete = "DELETE FROM tasks WHERE TaskId = @TaskId";

                using (MySqlConnection conn = new MySqlConnection(DBConnctString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(queryToDelete, conn))
                    {
                        // 1. Bind the parameter value
                        cmd.Parameters.AddWithValue("@TaskId", 1);

                        // 2. CRITICAL: Execute the command on the database
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Optional visual confirmation
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
    }
    }
