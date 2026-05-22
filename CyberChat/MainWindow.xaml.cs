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
    }
}