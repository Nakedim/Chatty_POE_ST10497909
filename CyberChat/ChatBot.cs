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
            
            _sentiment = sentimentDetector;
            _memory = memoryStore;
            sentiments = sentimentDetector;
            CurrentStatus = "Ask me About Cyber Security";
        }



        public string ProcessInput(string input)
        {
            CurrentStatus = "processing....";

            Sentiments mood = sentiments.Detect(input);

            string emotionReply = sentiments.GetSentimentsResponse(mood);
            if (string.IsNullOrWhiteSpace(input))
            {
                return "please rephrase";
            
            }

            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;
                CurrentStatus = $"Chatting with {input}";

                return $"Nice to Meet you{_memory.UserName}"
                    +$"Choose the following topics: {_keywords}"
                    ;
            }
            if (input.Contains("want"))
            {
                var keywords = _keywords.getAllKeywords();
                foreach (string keyword in keywords)
                {
                    _memory.FavouriteTopic = keyword;

                    return "Your fav keyword is " + keywords;
                }

                {

                }
            }

            return "enter your name to start chatting";

        }

        public string GetGreeting(string input)
        {

            return "";
        }




    }
}
