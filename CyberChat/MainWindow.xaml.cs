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
    public partial class MainWindow : Window
    {
        
        Media md = new Media();
        private ChatBot bot;
        public MainWindow()
        {
            InitializeComponent();
            BotQuestionText.Text = "My name is CyberChat, what is your Name";
            bot = new ChatBot(MsgInput);

            //Intros
            //Loaded += MainWindow_Loaded;
          
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

        private void MsgInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void MsgInput_KeyDown(object sender, KeyEventArgs e)
        {

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
        

        private void Send_Click(object sender, RoutedEventArgs e) 
        {
        
        }

        private void AnimateCursorLostFocus(object sender, RoutedEventArgs e)
        {
            //restore the place if the user leave the box
            if (string.IsNullOrEmpty(MsgInput.Text))
            {
                MsgInput.Text = Placeholder;
                MsgInput.Foreground = Brushes.Gray;
            }
        }

    }
}