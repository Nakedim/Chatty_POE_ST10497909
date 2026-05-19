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
        public MainWindow()
        {
            InitializeComponent();
        }

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

            
        }

        private string BotReplies(string message)
        {
            message = message.ToLower().Trim();
            string thread = "Typing";

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter your message");
            }
            if (message.Contains("hello"))
            {
                
                Thread.Sleep(1800);
                threadMimick();
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

        private string threadMimick()
        {
            return "typing";
        }
    }
}