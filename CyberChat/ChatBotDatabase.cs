using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberChat
{
    public class ChatBotDatabase
    {
    
         private string DBConnctString = "server=localhost;database=ChatBotDB;uid=root;pwd=Nakedim@dac702;";

        public void SaveToDatabase(string UserMessage, string BotMessage)
        {
            using (MySqlConnection connect = new MySqlConnection(DBConnctString))
            {
                try
                {
                    connect.Open();
                    string query = "INSERT INTO ChatHistory(UserMessage, BotMessage) Values(@UserName, @bot)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserMessage);
                        cmd.Parameters.AddWithValue("@Bot", BotMessage);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Cleaned up exception catch and appended the real error details for easier debugging
                    MessageBox.Show("Error saving into database: ChatHistory " + ex.Message);
                }
            }
        }

        public void TaskHandler(string Title, string Description, bool IsReminderSet)
        
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
INSERT INTO Tasks (title, description, is_reminder_set) 
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
                        //inserCMD.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                     
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
    }
    }
