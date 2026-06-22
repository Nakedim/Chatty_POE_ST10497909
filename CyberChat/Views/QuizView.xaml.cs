using CyberChat.QuizGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberChat.Views
{
    
    public partial class QuizView : UserControl
    {
        
        
        public QuizView()
        {
            InitializeComponent();
        }



        public void SetQuizContent(object sender, EventArgs e, string question)
        {
            QuestionTextBlock.Text = question;
            
            // You can add code here to display the options in the UI, e.g., in a ListBox or ComboBox.

        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string answer = AnswerTextBox.Text;
            MessageBox.Show($"Your answer: {answer}");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            AnswerTextBox.Clear();
        }
        private void CancelGame_click(object sender, RoutedEventArgs e)
        {
            
            ReturnToMain(this, new RoutedEventArgs());
        }
        private void newCyberChat(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.ChatBotArea.Items.Clear();
            ReturnToMain(this, new RoutedEventArgs());

        }
        public void ReturnToMain(object sender, RoutedEventArgs e)
        {
            // 1. Get a reference to your actual MainWindow
            var mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                // 2. Clear the quiz content from the container
                mainWindow.SubWindowContainer.Content = null;

                // 3. Turn the Chat Interface back VISIBLE, and hide the sub-container
                mainWindow.ChatInterfaceGrid.Visibility = Visibility.Visible;
                mainWindow.SubWindowContainer.Visibility = Visibility.Collapsed;
            }
        }
        public class TaskItem : INotifyPropertyChanged
        {
            private string _title;
            private bool _isCompleted;

            public string Title
            {
                get => _title;
                set { _title = value; OnPropertyChanged(); }
            }

            public bool IsCompleted
            {
                get => _isCompleted;
                set { _isCompleted = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
