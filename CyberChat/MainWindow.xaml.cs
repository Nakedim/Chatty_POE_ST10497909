using Chatty;
using System.Diagnostics.Eventing.Reader;
using System.Media;
using System.Text;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberChat
{
    public partial class MainWindow : Window
    {
   

        ChatBot chatBot;
        public MainWindow()
        {
            InitializeComponent();
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            GetGreeting();
           
        }

        public void InitializeChatBot()
        {
            // Create instances of KeywordResponder, SentimentDetector, and MemoryStore
            KeywordResponder keywordResponder = new KeywordResponder();
            SentimentDetector sentimentDetector = new SentimentDetector();
            MemoryStore memoryStore = new MemoryStore();
            // Initialize the ChatBot with the necessary dependencies
            chatBot = new ChatBot(MsgInput, keywordResponder, sentimentDetector, memoryStore);
        }

        // constructor: initialise _chatBot, play greeting, show ASCII art
        // SendButton_Click: call SendMessage()
        // UserInput_KeyDown: if Enter key, call SendMessage()
        // SendMessage(): read input, call _chatBot.ProcessInput(), display result
        // AppendBotMessage() / AppendUserMessage(): update ChatDisplay TextBlock

        public void LoadAsciiArt()
        {
            BotQuestionText.Text = @"";
        }

        public void GetGreeting()
        {
         string greeting = chatBot.GetGreeting();
        
            BotQuestionText.Text = greeting;

            // Call a method on your ChatBot class to get the greeting message
            // For example: string greeting = _chatBot.GetGreeting();
            // Then display it in the BotQuestionText TextBlock
            // BotQuestionText.Text = greeting;
        }   
        public void voiceGreeting()
        {
            SoundPlayer player = new SoundPlayer("greeting.wav");
            

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = MsgInput.Text.Trim();
            string Placeholder = "Type your message here...";

            // FIX: Just stop if empty. Do not save an empty string to MemoryStore.
            if (string.IsNullOrEmpty(userMessage) || userMessage == Placeholder)
            {
                MessageBox.Show("Please enter your message");
                return;
            }

            // FIRST TURN: Capture user name
           

                
                ChatBotArea.Items.Add("CyberChatBot: Nice to meet you "+userMessage);

               

            string botReply = chatBot.ProcessInput(userMessage);
            ChatBotArea.Items.Add("CyberChatBot: " + botReply);
            MsgInput.Clear();
            return;
            }



        private void MsgInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (string.IsNullOrWhiteSpace(MsgInput.Text) )
                {
                    return;
                }

                Send_Click(this, new RoutedEventArgs());
                e.Handled = true;
                MsgInput.Focus();
            }
        }
         string Placeholder = "Type your message here...";
        private void AnimateCursorGotFocus(object sender, RoutedEventArgs e)
            
        {
            if (MsgInput.Text == Placeholder)
            {
                MsgInput.Text = "";
                MsgInput.Foreground = Brushes.Black;
            }
        }

        private void AnimateCursorLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(MsgInput.Text))
            {
                MsgInput.Text = Placeholder;
                MsgInput.Foreground = Brushes.Gray;
            }
        }


    }
}