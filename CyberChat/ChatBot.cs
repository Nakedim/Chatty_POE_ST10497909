using Chatty;


namespace CyberChat
{
    public class ChatBot    
    {
        private readonly KeywordResponder _keywordResponder;
        private readonly SentimentDetector _sentimentDetector;
        private readonly MemoryStore _memoryStore;

        private bool _awaitingName = true;
        private string _lastTopic = "";

        public string CurrentStatus { get; private set; }

        public ChatBot(
            KeywordResponder keywordResponder,
            SentimentDetector sentimentDetector,
            MemoryStore memoryStore)
        {
            _keywordResponder = keywordResponder;
            _sentimentDetector = sentimentDetector;
            _memoryStore = memoryStore;

            CurrentStatus = "Ask me about Cyber Security";
        }

        public string ProcessInput(string input)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(input))
            {
                CurrentStatus = "Waiting for input";
                return "Please type something.";
            }

            string originalInput = input.Trim();
            string normalizedInput = originalInput.ToLower();

            CurrentStatus = "Processing...";

            // First interaction = ask for username
            if (_awaitingName)
            {
                return HandleUserName(originalInput);
            }

            // Store favourite topic
            if (normalizedInput.Contains("my favourite topic is"))
            {
                return SaveFavouriteTopic(originalInput);
            }

            // Recall favourite topic
            if (normalizedInput.Contains("what is my favourite topic"))
            {
                return RecallFavouriteTopic();
            }

            // Sentiment detection
            SentimentDetector.Sentiments mood = _sentimentDetector.Detect(normalizedInput);

            if (mood != SentimentDetector.Sentiments.Neutral)
            {
                CurrentStatus = "Responded to sentiment";
                return _sentimentDetector.GetSentimentsResponse(mood);
            }

            // Follow-up requests
            if (IsFollowUpRequest(normalizedInput))
            {
                return HandleFollowUpRequest();
            }

            // Basic chatbot questions
            string basicResponse = HandleBasicQuestions(normalizedInput);

            if (!string.IsNullOrEmpty(basicResponse))
            {
                CurrentStatus = "Answered basic question";
                return basicResponse;
            }

            // Keyword responses
            string keywordResponse =
                _keywordResponder.GetResponse(normalizedInput);

            if (!string.IsNullOrEmpty(keywordResponse))
            {
                _lastTopic = originalInput;

                CurrentStatus = "Topic discussed";
                return keywordResponse;
            }

            CurrentStatus = "Awaiting next question";

            return $"I'm not sure how to respond to that, {_memoryStore.UserName}. Ask me something about cyber security.";
        }

        private string HandleUserName(string userName)
        {
            _memoryStore.Store("name", userName);

            _awaitingName = false;

            CurrentStatus = $"Chatting with {userName}";

            return $"Nice to meet you {userName}! How are you?";
        }

        private string SaveFavouriteTopic(string input)
        {
            string topic =
                input.Replace("my favourite topic is", "",
                StringComparison.OrdinalIgnoreCase).Trim();

            if (string.IsNullOrWhiteSpace(topic))
            {
                return "Please tell me your favourite topic.";
            }

            _memoryStore.Store("topic", topic);
            //
            _lastTopic = topic;

            CurrentStatus = "Favourite topic saved";

            return $"Got it {_memoryStore.UserName}! Your favourite topic is {topic}.";
        }

        private string RecallFavouriteTopic()
        {
            string topic = _memoryStore.Recall("topic");

            if (string.IsNullOrEmpty(topic))
            {
                return "I do not know your favourite topic yet.";
            }

            return $"{_memoryStore.UserName}, your favourite topic is {topic}.";
        }

        private bool IsFollowUpRequest(string input)
        {
            //in case if user input has extra spaces and is case insensitive
            input.Trim().ToLower();
            //handles follow up requests
            return input.Contains("tell me more") ||
                   input.Contains("explain more");
        }

        private string HandleFollowUpRequest()
        {
            CurrentStatus = "Providing more information";

            if (!string.IsNullOrEmpty(_lastTopic))
            {
                return $"{_memoryStore.UserName}, here is more information about {_lastTopic}.";
            }

            return "Please ask about a cyber security topic first.";
        }

        private string HandleBasicQuestions(string input)
        {
            string normalizedInput= input.ToLowerInvariant();
      
            if (normalizedInput.Contains("how are you") ||
        normalizedInput.Contains("i'm good") ||
        normalizedInput.Contains("im good") ||
        normalizedInput.Contains("and you"))
            {
                return $"I'm functioning correctly and ready to help with Cyber Security question, {_memoryStore.UserName}.";
            }

            if (normalizedInput.Contains("what can you do"))
            {
                return $"I can help you with cyber security awareness, password safety, phishing, malware, and online protection, {_memoryStore.UserName}.";
            }

            return string.Empty;
        }
    }
}