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
                    MessageBox.Show("Error saving into database: " + ex.Message);
                }
            }
        }

        public void TaskHandler()
        
        {
            using (MySqlConnection conn = new MySqlConnection(DBConnctString))
            {
                try
                {
                    conn.Open();
                    //title, description, reminder

                    string sql = @"
            CREATE TABLE IF NOT EXISTS ChatLogs (
                id INT AUTO_INCREMENT PRIMARY KEY,
                UserInput TEXT NOT NULL,
                BotInput TEXT NOT NULL,
                Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            )";

                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error saving into database: " + ex.Message);
                }

            }
        }
    }
    }
