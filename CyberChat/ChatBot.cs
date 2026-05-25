using Chatty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public ChatBot(TextBox MsgInput,
                KeywordResponder keywordResponder,
                SentimentDetector sentimentDetector,
                MemoryStore memoryStore)
        {
            _keywords = keywordResponder;
            _sentiment = sentimentDetector;
            _memory = memoryStore;
        }


        public string ProcessInput(string input)
        {
            
           
            input = input.Trim();
            if (string.IsNullOrEmpty(input))
            {
             return "Enter your name" ;
            }
            if (_awaitingName) 
            {
                // Store the user's name in memory and return a personalized greeting from the input
               
                _username = input;
                _memory.UserName = _username;
                _awaitingName = false;
               return $"Welcome, {_memory.UserName}! How can I assist you today?";
            }

            return "Sorry, I didn't understand that. Can you please rephrase?";
           
            }
           
        


        public string GetGreeting()
        {
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


        public void advanceTopics(object sender, RoutedEventArgs e)
        {


            MessageBoxResult results = MessageBox.Show("Do you want to learn more about security?", "Next Topic", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (results == MessageBoxResult.Yes)
            {
                KeywordResponder kr = new KeywordResponder();
                string response = kr.GetResponse("password");
                kr.GetResponse("Password" + response);
            }

        }


        public void ResponseGenerator(string userInput)
        {
            
            
        }


    }
}
