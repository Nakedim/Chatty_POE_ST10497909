using CyberChat.QuizGame;
using System;
using System.Collections.Generic;
using System.Linq;
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
        QuizQuestions game = new QuizQuestions();
        public QuizView()
        {
            InitializeComponent();
            SetQuizContent(this, EventArgs.Empty, "");

        }



        public void SetQuizContent(object sender, EventArgs e, string question)
        {
            QuestionTextBlock.Text = question;
            // You can add code here to display the options in the UI, e.g., in a ListBox or ComboBox.
        }
    }
}
