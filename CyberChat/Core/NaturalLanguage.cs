using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CyberChat.Core
{


   public class NaturalLanguage
    {


        public string keywordPicker(string userInput)
        {
            userInput = userInput.ToLower();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                return string.Empty;
            }
            string normalizedInput = userInput.ToLowerInvariant();
            if (normalizedInput.Contains("task"))

            {
                return "task";
            }
            if (normalizedInput.Contains("reminder") || normalizedInput.Contains("schedule"))

            {
                return "reminder";
            }
            return string.Empty;
        }

       
    }
}
