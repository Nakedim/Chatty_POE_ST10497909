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
        User currentUser = new User();
        Media md = new Media();

        public MainWindow()
        {
            InitializeComponent();

            //Intros
            //The method will load immediate after mainApp have loaded
            Loaded += MainWindow_Loaded;



        }

        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
               
            md.PlaySound(currentUser);
            AppLogo.Text = md.logo;
        }


       //Chat conversation
       //handles the button event
        private void Send_Click(object sender, RoutedEventArgs e)
        {

          
            string userMessage = MessageInput.Text;

            //User Message

            ChatBotArea.Items.Add("You: " +userMessage);

            //Bot replies
            string botReply = BotReplies(userMessage);
            ChatBotArea.Items.Add("CyberChatBot: " + BotReplies("Hello everyone"));

            //

            MessageInput.Clear();
            MessageInput.Focus();

            
        }

       
        private string BotReplies(string message)
        {

            
            message = message.ToLower().Trim();
            

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter your message");
            }
            if (message.Contains("hello"))
            {
              
                return "hi";
            }
            else if (message.Contains("morning"))
            {
                return "Good Morning";
            }
            else
            {
                return "i didnt get get that please rephrase";
            }
        }

      

        //handle the enter key in case user enter to send msg 
        private void MessageInput_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return) 
            {
                if (string.IsNullOrWhiteSpace(MessageInput.Text) && MessageInput !=)
                {

                }
                Send_Click(this, new RoutedEventArgs());
                e.Handled = true;
                MessageInput.Clear();
                MessageInput.Focus();

            }

        }

        private readonly string PHolder;
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
    }
}