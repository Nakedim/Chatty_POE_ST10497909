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

            var UserQuiries = new Dictionary<string, string>
            {
                {"Task","activities" },
                {"2FA","Passwords" },
                {"Quiz","Game" },
                {"Remind me","Update Password" },
                {"play","activities" },
            };

            var UserGreeting = new Dictionary<string, string>
            {
                {"Hello","greeting" },
                {"Hey","Greeting" },
                {"Show","History" },
                {"chat History","log" },
                {"Task","scores" },
            };

            var ExitQuries = new Dictionary<string, string>
            {
                {"Quit","Cancel" },
                {"exit","abort" },
                {"Shutdown","poweroff" },
                {"bye","Goodbye" },
                {"leave","" },
            };
            var botDictionaries = new List<Dictionary<string, string>>
            {
                ExitQuries,UserGreeting,UserQuiries
            };
            var matchedEntries = botDictionaries
                .SelectMany(dict => dict)
                .FirstOrDefault(entry => userInput.Contains(entry.Key,
                StringComparison.OrdinalIgnoreCase));

                     
            return matchedEntries.Value?? string.Empty;
        }

        

       
    }
}
