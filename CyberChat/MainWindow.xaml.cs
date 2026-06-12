using Chatty;
using MySql.Data.MySqlClient;
using System;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChat
{
    public partial class MainWindow : Window
    {
        private ChatBot chatBot;
        private readonly string Placeholder = "Type your message here...";
        private MemoryStore _memory = new MemoryStore();
        private string DBConnctString = "server=localhost;database=ChatBotDB;uid=root;pwd=Nakedim@dac702;";

        public MainWindow()
        {
            InitializeComponent();
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            GetGreeting("");
        }

        private void InitializeChatBot()
        {
            chatBot = new ChatBot(
                new KeywordResponder(),
                new SentimentDetector(),
                new MemoryStore());
        }

        public void LoadAsciiArt()
        {
            AppLogo.Text = @" __       __   ___  __   __            ___  __   __  ___ 
/  ` \ / |__) |__  |__) /  ` |__|  /\   |  |__) /  \  |  
\__,  |  |__) |___ |  \ \__, |  | /~~\  |  |__) \__/  |  
                                                         ";
        }

        public void voiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Welcome.wav");
                player.Play();
            }
            catch (Exception)
            {
                MessageBox.Show("Error playing audio greeting");
            }
        }

        // Helper methods
        private void AddBotMessage(string BotInput, string UserInput)
        {
            ChatBotArea.Items.Add("CyberChatBot: " + BotInput);
            ChatBotArea.ScrollIntoView(ChatBotArea.Items[ChatBotArea.Items.Count - 1]);

            // FIXED: Flipped the arguments here so UserInput maps to UserMessage column
            SaveToDatabase(UserInput, BotInput);
        }

        private void AddUserMessage(string input, string UserName)
        {
            MemoryStore store = new MemoryStore();
            store.UserName = UserName;

            ChatBotArea.Items.Add("You: " + input);
            ChatBotArea.ScrollIntoView(ChatBotArea.Items[ChatBotArea.Items.Count - 1]);
        }

        private void SendMessage()
        {
            string UserMessage = MsgInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(UserMessage) || UserMessage == Placeholder)
            {
                AddBotMessage("Enter your name to proceed", "");
                return;
            }

            string botReply = chatBot.ProcessInput(UserMessage);
            AddUserMessage(UserMessage, "");
            AddBotMessage(botReply, UserMessage);
            MsgInput.Clear();
        }

        public string GetGreeting(string input)
        {
            ChatBotArea.Items.Add("CyberChatBot: Hello! I'm your Cyber Security assistant.");
            ChatBotArea.Items.Add("CyberChatBot: What’s your name?");
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return $"Hello ow can I assist you today?";
            }
            return string.Empty;
        }

        // Events methods
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void MsgInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void AnimateCursorGotFocus(object sender, RoutedEventArgs e)
        {
            if (MsgInput.Text.Trim() == Placeholder)
            {
                MsgInput.Text = "";
                MsgInput.Foreground = Brushes.Black;
            }
        }

        private void AnimateCursorLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MsgInput.Text))
            {
                MsgInput.Text = Placeholder;
                MsgInput.Foreground = Brushes.Gray;
            }
        }

        // Database methods
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
    }
}