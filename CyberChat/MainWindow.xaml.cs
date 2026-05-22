using Chatty;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Media md = new Media();

        public MainWindow()
        {
            InitializeComponent();
            BotQuestionText.Text = "My name is CyberChat, what is your Name";

            //Intros
            Loaded += MainWindow_Loaded;
          
            // ChatLogic
            // Chat logic methods goes here



        }

        // Intro, greeting, media (Sounds & logo) 
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //pause for testing
            //md.PlaySound(currentUser);
            AppLogo.Text = md.logo;
        }


        //Chat conversation
        //handles the button event
        private bool NameCaptured = false;
        
        private void Send_Click(object sender, RoutedEventArgs e)
        {



            string userMessage = MessageInput.Text.Trim();
           

            if (string.IsNullOrEmpty(userMessage))
            {
                MemoryStore.UserName = userMessage;
                MessageBox.Show("Please enter your message");
                return;
            }
            


            if (!NameCaptured)
            {
                MemoryStore.UserName = userMessage;
                NameCaptured = true;
                
                BotQuestionText.Text = $"Hello, {MemoryStore.UserName} How can i help you Today";
                ChatBotArea.Items.Add("CyberChatBot: Nice to meet you " + MemoryStore.UserName);
            MessageInput.Clear();
                return;

            }

            // Show user message
            ChatBotArea.Items.Add(MemoryStore.UserName + ": " + userMessage);

            // Bot reply
            string botReply = BotReplies(userMessage);

            ChatBotArea.Items.Add("CyberChatBot: " + botReply);

            // Clear input
            MessageInput.Clear();

        }

       
        private string BotReplies(string message)
        {
            Sentiments BotMood = new Sentiments();
            
            
            message = message.ToLower().Trim();

            

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter your message");
                return "";
            }


            if (message.Contains("hello"))
            {
                
                return "hi" + MemoryStore.UserName;
            }
            else if (message.Contains("morning"))
            {
                return "Good Morning" + MemoryStore.UserName;
            }
            if (message.Contains("questions"))
            {
                advanceTopics(this, new RoutedEventArgs());
                return "Opening advanced security topics...";
            }

            else
            {
                return "i didnt get get that please rephrase" + MemoryStore.UserName;
            }
        }



        //handle the enter key in case user enter to send msg 
        private readonly string PHolder = "Type Your Message...";
     
        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                // Stop if textbox is empty OR showing placeholder
                if (string.IsNullOrWhiteSpace(MessageInput.Text) ||
                    MessageInput.Text == PHolder)
                {
                    return;
                }

                Send_Click(this, new RoutedEventArgs());
                e.Handled = true;
                MessageInput.Focus();
            }
        }

        private void AnimateCursorGotFocus(object sender, RoutedEventArgs e)
        {
            if (MessageInput.Text == PHolder)
            {
                MessageInput.Text = "";
                MessageInput.Foreground = Brushes.Black;
            }
        }
        private void AnimateCursorLostFocus(object sender, RoutedEventArgs e) 
        {     //restore the place if the user leave the box
            if (string.IsNullOrEmpty(MessageInput.Text)) 
            {
                MessageInput.Text = PHolder;
                MessageInput.Foreground = Brushes.Gray;
            }
        }

        private void TextBoxBotArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        public void advanceTopics(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Do you want to learn more about security?  ");
           
            MessageBoxResult results = MessageBox.Show("Do you want to learn more about security?", "Next Topic", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (results == MessageBoxResult.Yes)
            {
             KeywordResponder kr = new KeywordResponder();
                string response = kr.GetResponse("password");
                kr.GetResponse("Password" +response);
            }
           
        }
    }
}