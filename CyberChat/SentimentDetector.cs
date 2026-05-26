using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        
        private readonly Dictionary<Sentiments,List<string> > SentimentList = 
         new Dictionary<Sentiments, List<string>>()
         {
             {Sentiments.Worried,
             new List<string> {"worried","scared","Afraid"}},
             {Sentiments.Neutral,
             new List<string >{"fine","ok","well"}},
             {Sentiments.Curious,
             new List<string>{ "why", "what", "how","?"}},
             {Sentiments.Frustrated,
             new List<string>{ "angry", "frustrated", "worried"," not happy","not good"}},
             {Sentiments.Happy,
             new List<string>{ "good", "well", "awesome","happy","excited"}},


         };

        public Sentiments Detect(string input)
        {
            input = input.ToLower();

            foreach(var sentiment in SentimentList)
            {
                foreach(string keyword in sentiment.Value)
                {
                    if (input.Contains(keyword))
                    {
                        return sentiment.Key;
                    }
                }
            }

            return Sentiments.Neutral;
        }

        
        
        public string GetSentimentsResponse(Sentiments sentiment)
        {
            switch (sentiment)
            {
                case Sentiments.Neutral:
                    return "Ohkay";
                case Sentiments.Worried:
                    return "Sad to Hear that";
                case Sentiments.Curious:
                    return "How can i help you";
                case Sentiments.Frustrated:
                    return "i can sense your frastrate, let solve it";
                case Sentiments.Happy:
                    return "Happy to hear that";

                default:
                    return "understand";

            }
        }

        
        }
    }

