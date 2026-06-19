using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChat.QuizGame
{
    public class QuizControl
    {
        public List<QuizQuestions> Questions { get; private set; }
        public int CurrentQuestionIndex { get; private set; } = 0;
        public int Score { get; private set; } = 0;

        public QuizControl()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            Questions = new List<QuizQuestions>
            {
                new QuizQuestions {
                    QuizQuestionText= "Is it safe to click a link in an email from an unknown sender if it looks urgent?",
                    Options = new List<string> { "True", "False" },
                    CorrectionOptions = 1,
                    Explanation = "Urgent language is a common social engineering tactic used in phishing emails to make you act without thinking."
                },
                new QuizQuestions {
                    QuizQuestionText = "Which of the following makes a password the most secure?",
                    Options = new List<string> { "Your pet's name", "12345678", "A long phrase mixing letters, numbers, and symbols", "Your birthdate" },
                    CorrectionOptions = 2,
                    Explanation = "Length and complexity are critical. Password phrases with mixed characters take exponentially longer for hackers to crack."
                }
                
            };
        }

        public QuizQuestions GetCurrentQuestion()
        {
            if (CurrentQuestionIndex < Questions.Count)
                return Questions[CurrentQuestionIndex];
            return null;
        }

        public bool SubmitAnswer(int selectedIndex, out string explanation)
        {
            var current = GetCurrentQuestion();
            explanation = current.Explanation;

            bool isCorrect = (selectedIndex == current.CorrectionOptions);
            if (isCorrect) Score++;

            CurrentQuestionIndex++; // Move to next question for later
            return isCorrect;
        }

        public string GetFinalFeedback()
        {
            double percentage = ((double)Score / Questions.Count) * 100;
            if (percentage >= 80)
                return $"Great job! You're a cybersecurity pro! Final Score: {Score}/{Questions.Count}";

            return $"Keep learning to stay safe online! Final Score: {Score}/{Questions.Count}";
        }
    }
}
