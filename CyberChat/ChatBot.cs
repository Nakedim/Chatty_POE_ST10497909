using Chatty;
using static CyberChat.SentimentDetector;

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

            // Handle first-time username setup
            if (_awaitingName)
            {
                return HandleUserName(originalInput);
            }

            // Detect user sentiment
            Sentiments mood = _sentimentDetector.Detect(normalizedInput);

            if (mood != Sentiments.Neutral)
            {
                CurrentStatus = "Responded to sentiment";
                return _sentimentDetector.GetSentimentsResponse(mood);
            }

            // Handle follow-up requests
            if (IsFollowUpRequest(normalizedInput))
            {
                return HandleFollowUpRequest();
            }

            // Handle common chatbot questions
            string basicResponse = HandleBasicQuestions(normalizedInput);

            if (!string.IsNullOrEmpty(basicResponse))
            {
                CurrentStatus = "Answered basic question";
                return basicResponse;
            }

            // Handle keyword-based responses
            string keywordResponse = _keywordResponder.GetResponse(normalizedInput);

            if (!string.IsNullOrEmpty(keywordResponse))
            {
                // Store the actual topic user asked about
                _lastTopic = originalInput;

                CurrentStatus = "Topic discussed";
                return keywordResponse;
            }

            // Default fallback response
            CurrentStatus = "Awaiting next question";

            return $"I'm not sure how to respond to that, {_memoryStore.UserName}. Ask me something about cyber security.";
        }

        private string HandleUserName(string userName)
        {
            _memoryStore.UserName = userName;
            _awaitingName = false;

            CurrentStatus = $"Chatting with {userName}";

            return $"Nice to meet you {userName}! How are you?";
        }

        private bool IsFollowUpRequest(string input)
        {
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
            if (input.Contains("how are you"))
            {
                return $"I'm functioning correctly and ready to help, {_memoryStore.UserName}.";
            }

            if (input.Contains("what can you do"))
            {
                return $"I can help you with cyber security awareness, password safety, phishing, malware, and online protection, {_memoryStore.UserName}.";
            }

            return string.Empty;
        }
    }
}