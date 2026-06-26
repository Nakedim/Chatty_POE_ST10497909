using Chatty;
using System.IO.Packaging;
using System.Windows;
using MySql.Data.MySqlClient;

namespace CyberChat
{
    public class ChatBot
    {
        private readonly KeywordResponder _responder;
        private readonly SentimentDetector _sentiment;
        private readonly MemoryStore _memory;
        private readonly TaskScheduler _task;
        private readonly ChatBotDatabase _database;
        private bool _awaitingName = true;
        private string _lastTopic = "";

        public string CurrentStatus { get; private set; }
        //string connectionString = "server=127.0.0.1;port=3306;database=CyberChatDB;uid=root;pwd=YOUR_PASSWORD;";

        public ChatBot(KeywordResponder responder, SentimentDetector
            sentiment, MemoryStore memory, ChatBotDatabase database, TaskScheduler Tasks)
        {
            _responder = responder;
            _sentiment = sentiment;
            _memory = memory;
            _database = database;
            _task = Tasks;
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

            input = input.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(input))
            {
                CurrentStatus = "Waiting for input";
                string emptyResponse = "Please type something.";
                SaveToDbQuietly(input, emptyResponse);
                return emptyResponse;
            }

            string originalInput = input.Trim();
            string normalizedInput = originalInput.ToLower();
            CurrentStatus = "Processing...";

            // 2. Determine the correct response based on logic
            string botMessage;

            // First interaction = ask for username 
            if (_awaitingName)
            {
                botMessage = HandleUserName(originalInput);
            }
            // Store favourite topic 
            else if (normalizedInput.Contains("my favourite topic is"))
            {
                botMessage = SaveFavouriteTopic(originalInput);
            }
            // Recall favourite topic 
            else if (normalizedInput.Contains("what is my favourite topic"))
            {
                botMessage = RecallFavouriteTopic();
            }
            // Sentiment detection 
            else if (_sentiment.Detect(normalizedInput) != SentimentDetector.Sentiments.Neutral)
            {
                SentimentDetector.Sentiments mood = _sentiment.Detect(normalizedInput);
                CurrentStatus = "Responded to sentiment";
                botMessage = _sentiment.GetSentimentsResponse(mood);
            }
            // Follow-up requests 
            else if (IsFollowUpRequest(normalizedInput))
            {
                botMessage = HandleFollowUpRequest();
            }
            // Basic chatbot questions 
            else if (!string.IsNullOrEmpty(HandleBasicQuestions(normalizedInput)))
            {
                CurrentStatus = "Answered basic question";
                botMessage = HandleBasicQuestions(normalizedInput);
            }
            // Keyword responses 
            else if (!string.IsNullOrEmpty(_responder.GetResponse(normalizedInput)))
            {
                _lastTopic = originalInput;
                CurrentStatus = "Topic discussed";
                botMessage = _responder.GetResponse(normalizedInput);
            }
            // Default fallback response
            else
            {
                CurrentStatus = "Awaiting next question";
                botMessage = $"I'm not sure how to respond to that, {_memory.UserName}. Ask me something about cyber security.";
            }

            // 3. Save the actual final message to the database before returning
            SaveToDbQuietly(input, botMessage);

            return botMessage;
        }

        // Helper method to keep database logging clean and reusable
        private void SaveToDbQuietly(string input, string response)
        {
            try
            {
                _database.SaveToDatabase(input, response);
                _database.TaskHandler(input, response, true);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error saving data to the database: " + e.Message);
            }
        }

        private string HandleUserName(string userName)

        {

             string Time = TimeOfDayResponse();
            _memory.Store("name", userName);

            _memory.Store("name", userName);

            _awaitingName = false;
            CurrentStatus = $"Chatting with {userName}";


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
            if (normalizedInput.Contains("how are you") || normalizedInput.Contains("i'm good") || normalizedInput.Contains("im good") || normalizedInput.Contains("and you"))
            {

                return $" I'm functioning correctly and ready to help with Cyber Security question, {_memory.UserName}.";

             

            }
            if (normalizedInput.Contains("what can you do"))
            {
                return $"I can help you with cyber security awareness, password safety, phishing, malware, and online protection, {_memory.UserName}.";
            }
            return string.Empty;
        }

        //Not part of assignment but added to make the bot more interactive and friendly.
        //part of my learning process to learn more about cshap features and how to use them in a practical way.
               


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
                case >= 18 and <= 23:
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
