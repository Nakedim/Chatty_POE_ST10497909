using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberChat
{
    public class Sentiments
    {

        public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }



        private readonly Dictionary<string, Sentiment> SentimentList = new();

        public string SentimentsDector()
        {
            for (int i = 0; i < SentimentList.Count; i++)
            {
                if(SentimentList.Values.ElementAt(i) == Sentiment.Worried)
                {
                    MessageBox.Show("The user is worried. Please provide support and reassurance.");
                }
                return SentimentList.Keys.ElementAt(i);
            }
            return "No sentiments detected.";
        }
        
        }
    }

