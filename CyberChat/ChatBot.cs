using Chatty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using static CyberChat.SentimentDetector;

namespace CyberChat
{
    internal class ChatBot
    {

        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;
        private SentimentDetector detector = new SentimentDetector();

        private SentimentDetector sentiments;
        private bool _awaitingName = true;
        private string _lastTopic;
        private string _username;

        public string CurrentStatus{  
            get; 
            private set; 
        }


        public ChatBot(
                KeywordResponder keywordResponder,
                SentimentDetector sentimentDetector,
                MemoryStore memoryStore)
        {
            _keywords = keywordResponder;
            _sentiment = sentimentDetector;
            _memory = memoryStore;
            sentiments = sentimentDetector;
            CurrentStatus = "Ask me About Cyber Security";
        }
        KeywordResponder keyResponse = new KeywordResponder();
        private readonly List<string> _cyberKeywords = new List<string>() { "Password", "Scam", "Malware", "Privacy", "Phishing" };

       
        public string ProcessInput(string input)
        {
            CurrentStatus = "processing....";
            // Analyze the user's input for sentiment and keywords, and generate an appropriate response based on the analysis and the chatbot's memory of the conversation
            SentimentDetector.Sentiments mood = sentiments.Detect(input);

            string emotionReply = sentiments.GetSentimentsResponse(mood);
            string keywordReply = _keywords.getGreetingResponse(input);
            CurrentStatus = "Ask Cyber Security questions";
                return emotionReply + "\n" + keywordReply;
           
            }





        public string GetGreeting()
        {
            //input = input.Trim();
            //BotQuestionText.Text = input;
            if (_awaitingName)
            {
                _username = _memory.UserName;
                if (string.IsNullOrEmpty(_username))
                {
                    return "Hello! What is your name?";
                }
                else
                {
                    _awaitingName = false;

                    return $"Welcome back, {_username}! How can I assist you today?";
                }
            }

            return "enter your name to start chatting";
        }




    }
}
