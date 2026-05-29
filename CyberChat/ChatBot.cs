using Chatty;
using static CyberChat.SentimentDetector;

namespace CyberChat
{
    public class ChatBot
    {

        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;
        

      
        private bool _awaitingName = true;
        private string _lastTopic = "";


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
            CurrentStatus = "Ask me About Cyber Security";
        }



        public string ProcessInput(string input)
        {
            input = input.ToLower();
            CurrentStatus = "processing....";
           
            Sentiments mood = _sentiment.Detect(input);
            
            if (string.IsNullOrWhiteSpace(input))
            {
                return "please type something";
            
            }
            //Order 1
            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;
                CurrentStatus = $"Chatting with {input}";

                return $"Nice to Meet you {_memory.UserName}! How are you?.";
                   
            }
          
            if (input.Contains("tell me more") 
                || input.Contains("explain more"))
            {
              if(!string.IsNullOrEmpty(_lastTopic))

                {
                  return $"{_memory.UserName} Here is the information about {_lastTopic}";

                    //ivoke a dictionary
                }
        
            }
            //Order 3
            mood = _sentiment.Detect(input);

            //if it is not neutral, it should return null
            if (mood != Sentiments.Neutral)
            {
                return _sentiment.GetSentimentsResponse(mood);
                
            }
            
            //Order 4

            if (input.IndexOf("how are you", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return $"I'm functioning correctly and ready to help. {_memory.UserName}";
            }
            if (input.IndexOf("what can you do", StringComparison.OrdinalIgnoreCase) >=0)
            {
                return $"i can help you with cyber security matters.{_memory.UserName}";
            }
            //Order 5
            string response = _keywords.GetResponse(input);

            if (!string.IsNullOrEmpty(response))
            {
                //store the last topic
                _lastTopic = response;
                return response;
            }
           
            //Order 6
            return $"ask me about  {_memory.UserName}";
        }

   




    }
}
