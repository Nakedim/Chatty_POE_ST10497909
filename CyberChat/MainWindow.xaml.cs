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
        
        public MainWindow()
        {
            InitializeComponent();
<<<<<<< HEAD
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            GetGreeting("");
          





        }

        private void InitializeChatBot()
        {

           
=======
>>>>>>> f66c6af9915a04f62757e4e61b279dda79f9a071
            chatBot = new ChatBot(
                new KeywordResponder(),
                new SentimentDetector(),
                new MemoryStore(),
                //for the part 3
                new ChatBotDatabase()
                );

            LoadAsciiArt();
            voiceGreeting();

            string BotGreeting = chatBot.GetGreeting("hello");
            ChatBotArea.Items.Add($"CyberChatBot: {BotGreeting}");
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
        }

        private void AddUserMessage(string input, string UserName)
        {
            MemoryStore store = new MemoryStore();
            store.UserName = UserName;
            ChatBotDatabase database = new ChatBotDatabase();
            ChatBotArea.Items.Add("You: " + input);
           
            ChatBotArea.ScrollIntoView(ChatBotArea.Items[ChatBotArea.Items.Count - 1]);
        }
        private readonly string Placeholder = "Type your message here...";
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


       
    }
}