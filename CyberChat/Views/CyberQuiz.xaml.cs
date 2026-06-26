using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberChat.Views
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; } // Null or Empty implies True/False structure
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }

    public partial class CyberQuiz : UserControl
    {
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int userScore = 0;

        public CyberQuiz()
        {
            InitializeComponent();
            intializeQuizData();
            LoadCurrentQuestion();
        }

        private void intializeQuizData()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "Phishing attacks only happen via traditional email correspondence.",
                    CorrectAnswer = "False",
                    Explanation = "Smishing (SMS phishing) and Vishing (voice phishing) target phones and messaging platforms."
                },
                new QuizQuestion {
                    QuestionText = "Which element makes an account password the strongest against crack attempts?",
                    Options = new List<string> { "Using your birthday backwards", "A long phrase mixing words, numbers, and symbols", "Replacing letters with simple numbers (like 'E' to '3')" },
                    CorrectAnswer = "A long phrase mixing words, numbers, and symbols",
                    Explanation = "Entropy and password length represent the best barriers against automated brute force dictionaries."
                },
                new QuizQuestion {
                    QuestionText = "Two-Factor Authentication (2FA) completely guarantees your account can never be breached.",
                    CorrectAnswer = "False",
                    Explanation = "While 2FA adds highly critical safety barriers, sophisticated session hijacking or real-time phishing can bypass it."
                },
                new QuizQuestion {
                    QuestionText = "Unscramble this letter wsdspra.",
                 
                    Options = new List<string> { "Password", "Drawpass", "Wadpress", "Sparsward" },
                    CorrectAnswer = "Password",
                    Explanation = "Unscrambling 'wsdspra' spells out 'Password'. Make sure your passwords are complex and long."
                },
                new QuizQuestion {
                    QuestionText = "Which one is an active virus threat classification?",
                    
                    Options = new List<string> { "Trojan Horse", "Phishing", "Social Engineering", "Adware Cookie" },
                    CorrectAnswer = "Trojan Horse",
                    Explanation = "A Trojan Horse infects computers, phones, and networks by masquerading as legitimate software."
                },
                new QuizQuestion {
                    QuestionText = "Which of the following techniques is an example of social engineering?",
                   
                    Options = new List<string> { "CEO Fraud", "SQL Injection", "Brute Force Attack", "Firewall Blocking" },
                    CorrectAnswer = "CEO Fraud",
                    Explanation = "CEO Fraud uses manipulation and impersonation tactics to bypass secure organizational perimeters."
                },
                     new QuizQuestion {
                    QuestionText = "choose the worse practice for online safety",

                    Options = new List<string> { "share password","use strong passwords","avoid suspicious links" },
                    CorrectAnswer = "share password",
                    Explanation = "Sharing passwords compromises account security and should be avoided."
                },
                    new QuizQuestion {
                    QuestionText = "Computer virus are smilar to human viruses",

                    Options = new List<string> { "True", "False" },
                    CorrectAnswer = "true",
                    Explanation = "Computer viruses are similar to human viruses in that they both replicate and spread from one host to another."
                },
                         new QuizQuestion {
                    QuestionText = "Choose methods to keep your online accounts secure",

                    Options = new List<string> { "Use strong, unique passwords for each account", "Enable two-factor authentication", "Be cautious of suspicious emails and links", "All of the above" },
                    CorrectAnswer = "All of the above",
                    Explanation = "Using strong, unique passwords, enabling two-factor authentication, and being cautious of suspicious emails and links are all effective methods to keep your online accounts secure."
                },
                              new QuizQuestion {
                    QuestionText = "what is VPN",

                    Options = new List<string> { "Virtual Private Network", "Visible Private Network", "Virus Protection Network", "Video Processing Node" },
                    CorrectAnswer = "Virtual Private Network",
                    Explanation = "A VPN (Virtual Private Network) creates a secure connection over a public network, allowing users to access resources as if they were directly connected to a private network."
                },
                    new QuizQuestion {
                    QuestionText = "what is SSL",

                    Options = new List<string> { "Secure Sockets Layer", "Super Simple Layer", "System Security Layer", "Safe Storage Link" },
                    CorrectAnswer = "Secure Sockets Layer",
                    Explanation = "SSL (Secure Sockets Layer) is a protocol for establishing encrypted links between web servers and browsers."
                }
            };
        }

        private void LoadCurrentQuestion()
        {
            FeedbackPanel.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Collapsed;
            OptionsPanel.Children.Clear();

            if (quizQuestions == null || quizQuestions.Count == 0)
            {
                MessageBox.Show("Quiz data collection is empty.", "Logic Error");
                return;
            }

            if (currentQuestionIndex >= quizQuestions.Count)
            {
                DisplayFinalScoreSummary();
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            ProgressTextBlock.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}";
            ScoreTextBlock.Text = $"Score: {userScore}";
            QuestionTextBlock.Text = question.QuestionText;

            // Generate options based on dynamic question type
            List<string> dynamicOptions = (question.Options != null && question.Options.Count > 0)
                ? question.Options
                : new List<string> { "True", "False" };

            foreach (var option in dynamicOptions)
            {
                RadioButton rb = new RadioButton
                {
                    Content = option,
                    Margin = new Thickness(0, 6, 0, 6),
                    Foreground = Brushes.White,
                    FontSize = 13
                };
                OptionsPanel.Children.Add(rb);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRb = null;
            foreach (var child in OptionsPanel.Children)
            {
                if (child is RadioButton rb && rb.IsChecked == true)
                {
                    selectedRb = rb;
                    break;
                }
            }

            if (selectedRb == null)
            {
                MessageBox.Show("Please choose an option before submitting.", "Selection Required");
                return;
            }

            var question = quizQuestions[currentQuestionIndex];
            bool isCorrect = selectedRb.Content.ToString() == question.CorrectAnswer;

            if (isCorrect)
            {
                userScore++;
                ScoreTextBlock.Text = $"Score: {userScore}";
                FeedbackResultText.Text = "Correct! ✅";
                FeedbackResultText.Foreground = Brushes.Green;
            }
            else
            {
                FeedbackResultText.Text = "Incorrect ❌";
                FeedbackResultText.Foreground = Brushes.Red;
            }

            ExplanationText.Text = question.Explanation;
            FeedbackPanel.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Visible;

            // Lock inputs until next screen item actioned
            foreach (Control child in OptionsPanel.Children) child.IsEnabled = false;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }

        private void DisplayFinalScoreSummary()
        {
            string finalEvaluation = (userScore >= (quizQuestions.Count * 0.75))
                ? "Great job! You're a cybersecurity pro!"
                : "Keep learning to stay safe online!";

            MessageBox.Show($"Quiz complete!\nYour Final Score: {userScore} / {quizQuestions.Count}\n\n{finalEvaluation}", "Game Result Summary");
            ReturnToMain(this, null);
        }

        private void CancelGame_click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the current quiz?", "Exit Game", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ReturnToMain(sender, e);
            }
        }

        private void ReturnToMain(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (mainWindow != null)
            {
                // Clear the quiz content cleanly
                mainWindow.SubWindowContainer.Content = null;
                mainWindow.SubWindowContainer.Visibility = System.Windows.Visibility.Collapsed;

                // Restore your chatbot interface panel 
                mainWindow.ChatInterfaceGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
