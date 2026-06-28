using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberChat.Core
{
    public class NaturalLanguage
    {
        public string keywordPicker(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return string.Empty;
            }

            // CLEANED: Keys are lowercase to guarantee accurate substring match lookup executions
            var UserQuiries = new Dictionary<string, string>
            {
                {"task", "activities"},
                {"2fa", "Passwords"},
                {"quiz", "Game"},
                {"remind me", "Update Password"},
                {"play", "activities"}
            };

            var UserGreeting = new Dictionary<string, string>
            {
                {"hello", "greeting"},
                {"hey", "Greeting"},
                {"show", "History"},
                {"chat history", "log"}
            };

            var ExitQueries = new Dictionary<string, string>
            {
                {"quit", "Cancel"},
                {"exit", "abort"},
                {"shutdown", "poweroff"},
                {"bye", "Goodbye"}
            };

            var botDictionaries = new List<Dictionary<string, string>>
            {
                ExitQueries, UserGreeting, UserQuiries
            };

            // Scans dictionaries for any matching substring inside the user input
            var matchedEntry = botDictionaries
                .SelectMany(dict => dict)
                .FirstOrDefault(entry => userInput.Contains(entry.Key, StringComparison.OrdinalIgnoreCase));

            return matchedEntry.Value ?? string.Empty;
        }
    }
}
