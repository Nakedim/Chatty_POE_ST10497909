namespace CyberChat
{
    public class SentimentDetector
    {

        public enum Sentiments {
            Neutral,
            Worried, 
            Curious,
            Frustrated,
            Happy }
        
        private readonly Dictionary<Sentiments,List<string> > sentimentList = 
         new Dictionary<Sentiments, List<string>>()
         {
             {Sentiments.Worried,
             new List<string> {"worried","scared","Afraid,","concerned","sad"}},
             {Sentiments.Neutral,
             new List<string >{""}}, 
             {Sentiments.Curious,
             new List<string>{ "why", "what", "how","?"}},
             {Sentiments.Frustrated,
             new List<string>{ "angry", "frustrated", "worried","worry","not"}},
             {Sentiments.Happy,
             new List<string>{ "good", "well", "awesome","happy","excited"}},


         };

        public Sentiments Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Sentiments.Neutral;
            }


            input = input.ToLower();

            foreach(var sentiment in sentimentList)
            {
                bool found = sentiment.Value.Any(key => input.Contains(key.ToLower()));
                if (found)
                {
                    return sentiment.Key;
                }
              
            }
            return Sentiments.Neutral;
           
        }

        
        
        public string GetSentimentsResponse(Sentiments mood)
        {
            switch (mood)
            {
                case Sentiments.Neutral:
                    return null;
                case Sentiments.Worried:
                    return "Sad to Hear that, dont worry i will help you";
                case Sentiments.Curious:
                    return "How can i help you";
                case Sentiments.Frustrated:
                    return "i can sense your frustration, lets solve it";
                case Sentiments.Happy:
                    return "Happy to hear that";

                default:
                    return "";

            }
        }

        
        }
    }

