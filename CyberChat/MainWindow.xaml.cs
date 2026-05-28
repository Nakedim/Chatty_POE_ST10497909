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
   
           private ChatBot chatBot;
             private readonly string  Placeholder = 
            "Type your message here...";
             
        public MainWindow()
        {
            InitializeComponent();
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            setStatus(chatBot.GetGreeting(""));
 
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

            SoundPlayer player = new SoundPlayer("Welcome.wav");
            player.Play();
        }

        //helper methods
        private void AddBotMessage(string input)

        {
            ChatBotArea.Items.Add("CyberChatBot: " + input);
        }

        private void AddUserMessage(string input, string UserName)
        {
            MemoryStore store = new MemoryStore();
            store.UserName = UserName;

            ChatBotArea.Items.Add("You: " + input);
            ChatBotArea.ScrollIntoView(ChatBotArea.Items[ChatBotArea.Items.Count - 1]);
        }


        private void setStatus(string message)
        {
            BotQuestionText.Text = message;
        }
       
      private void SendMessage()
        {
            string UserMessage = MsgInput.Text.Trim();
            string botReply = chatBot.ProcessInput(UserMessage);

            if (string.IsNullOrWhiteSpace(UserMessage) || UserMessage == Placeholder)
            {
              
                AddBotMessage("Enter your name to proceed");
                             return;
            }

                AddUserMessage(UserMessage, "");
                AddBotMessage(botReply);
                setStatus(chatBot.CurrentStatus);
                MsgInput.Clear();
            }
        
       


        //Events methods

        private void Send_Click(object sender, RoutedEventArgs e)
            {
            SendMessage();
            }


        private void MsgInput_KeyDown(
            object sender, KeyEventArgs e)
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