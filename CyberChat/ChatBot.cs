using Chatty;
using System;
using System.Windows.Controls;
using static CyberChat.SentimentDetector;

namespace CyberChat
{
    internal class ChatBot
    {
        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;

        private bool _awaitingName = true;
        private string _lastTopic;
        private string _username;

        public string CurrentStatus { get; private set; }

        public ChatBot(
            KeywordResponder keywordResponder,
            SentimentDetector sentimentDetector,
            MemoryStore memoryStore)
        {
            _keywords = keywordResponder;
            _sentiment = sentimentDetector;
            _memory = memoryStore;
            CurrentStatus = "Ask me About Cyber Security";
        }

        public string ProcessInput(string input)
        {
            CurrentStatus = "processing....";

            if (string.IsNullOrWhiteSpace(input))
            {
                return "please type something";
            }

            input = input.ToLower();
            Sentiments mood = _sentiment.Detect(input);

            // Order 1: Ask for name
            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;
                CurrentStatus = $"Chatting with {input}";
                return $"Nice to meet you {_memory.UserName}! Ask me about cyber security.";
            }

            // Order 2: "Tell me more"
            if (input.Contains("tell me more") || input.Contains("explain more"))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    return $"Here is the information about {_lastTopic}";
                }
            }

            // Order 3: Sentiment response
            if (mood != Sentiments.Neutral)
            {
                return _sentiment.GetSentimentsResponse(mood);
            }

            // Order 4: Small talk (moved before keyword detection)
            if (input.Contains("how are you"))
            {
                return "I'm functioning correctly and ready to help.";
            }
            if (input.Contains("what can you do"))
            {
                return "I can help you with cybersecurity topics like phishing, scams, and password safety.";
            }

            // Order 5: Keyword response
            string response = _keywords.GetResponse(input);
            if (!string.IsNullOrEmpty(response))
            {
                _lastTopic = response;
                return response;
            }

            // Order 6: Default fallback
            return "Ask me about cybersecurity.";
        }

        public string GetGreeting(string input)
        {
            input = input.ToLower();
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return $"Hello {_memory.UserName}, how can I assist you today?";
            }
            return string.Empty;
        }
    }
}
