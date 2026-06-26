namespace CyberChat.Core
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
             new List<string> {"unhappy","scared","worry", "worried","afraid,","concerned","sad","bother"}},
             {Sentiments.Curious,
             new List<string>{ "Wonder", "Curious", "Inquisitive", "Intrigued", "Inquiring"}},
             {Sentiments.Frustrated,
             new List<string>{ "angry", "frustrated", "confused","annoyed", "irrated","troubled"}},
             {Sentiments.Happy,
             new List<string>{ "good", "well", "awesome","happy","excited"}},


         };

   
        
        public string GetSentimentsResponse(Sentiments mood)
        {

            switch (mood)
            {
                case Sentiments.Neutral:
                    return string.Empty;
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

        public Sentiments Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Sentiments.Neutral;
            }


            input = input.ToLower();
            string cleanInput = input.ToLowerInvariant();
            foreach (var sentiment in sentimentList)
            {
                bool found = sentiment.Value.Any(key => input.Contains(key.ToLower()));
                if (found)
                {
                    return sentiment.Key;
                }

            }
            return Sentiments.Neutral;

        }


    }
}

