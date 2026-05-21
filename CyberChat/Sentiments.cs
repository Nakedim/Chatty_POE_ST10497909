using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChat
{
    public class Sentiments
    {

        public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }



        private readonly Dictionary<string, Sentiment> SentimentList = new();

        public string SentimentsDector()
        {
            string mgs = "";

            if (mgs.Contains("happy")) 
            {
               
            }
            return mgs;
        }
        }
    }

