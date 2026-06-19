using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChat.QuizGame
{
    public class QuizQuestions
    {
        //hold questions
        public string QuizQuestionText{ get; set; }

        //hold true and false;
        public List<string> Options { get; set; }
        //correctAnswer
        public int CorrectionOptions { get; set; }
        //feedback
        public string Explanation { get; set; }
    }
}
