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
using static CyberChat.SentimentDetector;

namespace CyberChat
{
    public partial class MainWindow : Window
    {
   

        ChatBot chatBot;
        private SentimentDetector detector = new SentimentDetector();
        public MainWindow()
        {
            InitializeComponent();
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            GetGreeting("");
           
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

        public void GetGreeting(string input)
        {
            // 1. Find the matched key from the dictionary keys
            string matchedKey = KeywordResponder.BotQuestions.Keys
                .FirstOrDefault(q => input.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);

            // 2. Check if a match was successfully found
            if (matchedKey != null)
            {
                // Get the list of responses tied to that key
                List<string> associatedResponses = KeywordResponder.BotQuestions[matchedKey];

                // Grab the first dynamic response text from the list
                string dynamicResponse = associatedResponses.FirstOrDefault() ?? "No answer configured.";

                // Update your black UI background text control with the dynamic response
                BotQuestionText.Text = dynamicResponse;
            }
            else
            {
                // Fallback: If no keywords match, use your default greeting method
                BotQuestionText.Text = chatBot.GetGreeting();
            }
        }
        public void voiceGreeting()
        {
            SoundPlayer player = new SoundPlayer("greeting.wav");
            

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            
            string userMessage = MsgInput.Text.Trim();
            string Placeholder = "Type your message here...";

            Sentiments sentiments = detector.Detect(userMessage);
            string emotionReply = detector.GetSentimentsResponse(sentiments);
            

            // FIX: Just stop if empty. Do not save an empty string to MemoryStore.
            if (string.IsNullOrEmpty(userMessage) || userMessage == Placeholder)
            {
                //MessageBox.Show("Please enter your message");
                //color the message red
                ChatBotArea.Items.Add("Please Enter your name on the provide box" + userMessage);

                return;
            }
                //ChatBotArea.Items.Add(emotionReply+ ", CyberChatBot: Nice to meet you "+userMessage);
                ////ChatBotArea.Items.Add("CyberChatbot: " + emotionReply);

               

            string botReply = chatBot.ProcessInput(userMessage);
           
            ChatBotArea.Items.Add("CyberChatBot: " + botReply);
            MsgInput.Clear();
            return;
            }

        private readonly string Placeholder = "Type your message here...";

        private void MsgInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (string.IsNullOrWhiteSpace(MsgInput.Text) || MsgInput.Text ==Placeholder)
                {
                    return;
                }

                Send_Click(this, new RoutedEventArgs());
                e.Handled = true;
                MsgInput.Focus();
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