using Chatty;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChat
{
    public partial class MainWindow : Window
    {
   
           private ChatBot chatBot;
             private readonly string  Placeholder = 
            "Type your message here...";
        private MemoryStore _memory = new MemoryStore();

        public MainWindow()
        {
            InitializeComponent();
            InitializeChatBot();
            LoadAsciiArt();
            voiceGreeting();
            GetGreeting("");
            chatBot.TimeOfDayResponse(0);





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