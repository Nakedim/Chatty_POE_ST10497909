using System.IO.Packaging;
using System.Windows;

using MySql.Data.MySqlClient;

namespace CyberChat.Core
{
    public class ChatBot
    {
        private readonly KeywordResponder _responder;
        private readonly SentimentDetector _sentiment;
        private readonly MemoryStore _memory;
        private readonly TaskScheduler _task;
        private readonly ChatBotDatabase _database;
        private readonly NaturalLanguage _NLP;
        private bool _awaitingName = true;
        private string _lastTopic = "";

        public string CurrentStatus { get; private set; }
        //string connectionString = "server=127.0.0.1;port=3306;database=CyberChatDB;uid=root;pwd=YOUR_PASSWORD;";

        public ChatBot(KeywordResponder responder, SentimentDetector
            sentiment, MemoryStore memory,ChatBotDatabase database, TaskScheduler Tasks, 
            NaturalLanguage nlp)
        {
            _responder = responder;
            _sentiment = sentiment;
            _memory = memory;
            _task = Tasks;
            _NLP = nlp;
            _database = database;
        }
        public string GetGreeting(string input)
        {
            try
            {
                if (_awaitingName)
            {
                return "CyberChatBot: Hello! I'm your Cyber Security assistant.What’s your name?";
            }
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return $"Hello ow can I assist you today?";
            }
           
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error reading" + e.Message);
            }
        return string.Empty;
            
        }
        public string ProcessInput(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                CurrentStatus = "Waiting for input";
                string emptyResponse = "Please type something.";
                //SaveLogToDatabase(string.Empty, emptyResponse);
                return emptyResponse;
            }

            string originalInput = input.Trim();
            string normalizedInput = originalInput.ToLowerInvariant();
            CurrentStatus = "Processing...";

            string botMessage;
            string actionKeyword;

            // PRIORITY 1: Identity & Authentication Initialization
            if (_awaitingName)
            {
                string sanitizedName = System.Web.HttpUtility.HtmlEncode(originalInput);
                botMessage = HandleUserName(sanitizedName);
            }
            // PRIORITY 2: Contextual Topic Memory Management
            else if (normalizedInput.Contains("my favourite topic is"))
            {
                botMessage = SaveFavouriteTopic(originalInput);
            }
            else if (normalizedInput.Contains("what is my favourite topic"))
            {
                botMessage = RecallFavouriteTopic();
            }
            // PRIORITY 3: General Dynamic Context Follow-Ups
            else if (IsFollowUpRequest(normalizedInput))
            {
                botMessage = HandleFollowUpRequest();
            }
            // PRIORITY 4: Direct Action Triggers (WPF UI Windows / Security Tasks)
            else if (!string.IsNullOrEmpty(actionKeyword = _NLP.keywordPicker(normalizedInput)))
            {
                 string safeUser = _memory.Recall("name") ?? "User";

                if (actionKeyword =="activities" || actionKeyword =="scores")
                {
                    CurrentStatus = "Scheduling Task";

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _task.Show();
                });
                    botMessage = $"{safeUser}, I'm happy to assist you to set your task.";
                }
                // 2. Matches "Passwords" (from "2FA") or "Update Password" (from "Remind me")
                else if (actionKeyword.Equals("Passwords", StringComparison.OrdinalIgnoreCase) ||
                         actionKeyword.Equals("Update Password", StringComparison.OrdinalIgnoreCase))
                {
                    botMessage = $"{safeUser}, let's look at your security credentials. Please use strong 2FA configurations.";
                }
                // 3. Matches "Game" (from "Quiz")
                else if (actionKeyword.Equals("Game", StringComparison.OrdinalIgnoreCase))
                {
                    botMessage = $"Starting the security quiz game for you now, {safeUser}!";
                }
                // 4. Matches "Cancel", "abort", "poweroff", "Goodbye", or "" (from your ExitQueries dictionary)
                else
                {
                    CurrentStatus = "Exiting Chat";
                    botMessage = $"Goodbye {safeUser}! Stay safe online.";
                }
               
                botMessage = $"{safeUser}, I'm happy to assist you to set your task.";
            }
            // PRIORITY 5: Exact Infrastructure Keyword Matches 
            else if (!string.IsNullOrEmpty(botMessage = _responder.GetResponse(normalizedInput)))
            {
                _lastTopic = originalInput;
                CurrentStatus = "Topic discussed";
            }
            // PRIORITY 6: Static Informational Structural Questions
            else if (!string.IsNullOrEmpty(botMessage = HandleBasicQuestions(normalizedInput)))
            {
                CurrentStatus = "Answered basic question";
            }
            // PRIORITY 7: Sentiment & Urgency Assessment (Natural Fallback)
            // Placed at the bottom so words like "confused" don't break security commands.
            else if (_sentiment.Detect(normalizedInput) != SentimentDetector.Sentiments.Neutral)
            {
                SentimentDetector.Sentiments mood = _sentiment.Detect(normalizedInput);
                CurrentStatus = "Responded to sentiment";
                botMessage = _sentiment.GetSentimentsResponse(mood);
            }
            // PRIORITY 8: Catch-All System Fallback
            else
            {
                CurrentStatus = "Awaiting next question";
                string safeUser = _memory.Recall("name") ?? "User";
                botMessage = $"I'm not sure how to respond to that, {safeUser}. Ask me something about cyber security.";
            }

            // Secure database log execution
            _database.SaveLogToDatabase(originalInput, botMessage);

            return botMessage;
        }

    

        private string HandleUserName(string userName)

        {

             string Time = TimeOfDayResponse();
            _memory.Store("name", userName);

            _memory.Store("name", userName);

            _awaitingName = false;
           
            return $"{Time}{userName},Nice to meet you! How are you?";


        }

        private string SaveFavouriteTopic(string input)
        {
            string topic = input.Replace("my favourite topic is", "", StringComparison.OrdinalIgnoreCase).Trim();
            if (string.IsNullOrWhiteSpace(topic))
            {
                return "Please tell me your favourite topic.";
            }
            _memory.Store("topic", topic);
            CurrentStatus = "Favourite topic saved";
            return $"Got it {_memory.UserName}! Your favourite topic is {topic}.";
        }

        private string RecallFavouriteTopic()
        {
            string topic = _memory.Recall("topic");
            if (string.IsNullOrEmpty(topic))
            {
                return "I do not know your favourite topic yet.";
            }
            return $"{_memory.UserName}, your favourite topic is {topic}.";
        }

        private bool IsFollowUpRequest(string input)
        {

            //in case if user input has extra spaces and is case insensitive
            input = input.Trim().ToLower();
            //handles follow up requests
            return input.Contains("tell me more") || input.Contains("explain more");
                   
        }

        private string HandleFollowUpRequest()
        {
            CurrentStatus = "Providing more information";
            if (!string.IsNullOrEmpty(_lastTopic))
            {
                return $"{_memory.UserName}, here is more information about {_lastTopic}.";
            }
            return "Please ask about a cyber security topic first.";
        }

        private string HandleBasicQuestions(string input)
        {
            string normalizedInput = input.ToLowerInvariant();
            if (normalizedInput.Contains("how are you") 
                || normalizedInput.Contains("and you"))
            {

                return $" I'm functioning correctly and ready to help with Cyber Security question, {_memory.UserName}.";

             

            }
            if (normalizedInput.Contains("what can you do"))
            {
                return $"I can help you with cyber security awareness, password safety, phishing, malware, and online protection, {_memory.UserName}.";
            }
            return string.Empty;
        }


          //as part of NLP
      public string TimeOfDayResponse(){
            int hour = DateTime.Now.Hour;

            string timeOfDayResponse = " ";
            switch (hour)
            {
                case >= 0 and <= 11:
                    timeOfDayResponse = "Good morning!";
                    break;
                case >= 12 and <= 17:
                    timeOfDayResponse = "Good Afternoon!";
                    break;
                case >= 18 and <= 24:
                    timeOfDayResponse = "Good Evening!";
                    break;
                default:
                    timeOfDayResponse = "";
                    break;
            }
            return timeOfDayResponse;
        }
    }
}
