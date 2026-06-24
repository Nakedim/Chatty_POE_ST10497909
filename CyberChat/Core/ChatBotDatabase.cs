using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
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
                string queryToDelete = "DELETE FROM tasks WHERE taskId = 4";
                

                using (MySqlConnection conn = new MySqlConnection(DBConnctString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(queryToDelete, conn))
                    {
                        
                        cmd.Parameters.Add("@TaskId",MySqlDbType.Int32, taskId).Value = taskId;

         
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

        public void ListMyDb()
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
                        //step 3: create a command and execute it
                        MySqlDataAdapter adapter = new MySqlDataAdapter(sqlQuery, conn);
                        DataTable dt = new DataTable();

                        //fill the datatable with the results of the query
                        adapter.Fill(dt);
                        StringBuilder sb = new StringBuilder();

                        foreach (DataRow row in dt.Rows)
                        {
                            string title = row["title"].ToString();
                            string description = row["description"].ToString();
                            bool isReminderSet = Convert.ToBoolean(row["is_reminder_set"]);
                            DateTime timestamp = Convert.ToDateTime(row["timestamp"]);
                            // Display the data in a message box or console

                            MessageBox.Show($"Title: {title}\nDescription: {description}\nIs Reminder Set: {isReminderSet}\nTimestamp: {timestamp}");
                            sb.Append($"Title: {title}");

                        }
                   

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Database listeing error" +e.Message);
                    }

                }
            }
           
        }
    }
    }
