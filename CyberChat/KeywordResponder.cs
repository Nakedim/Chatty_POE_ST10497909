using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CyberChat
{
   public class KeywordResponder
    {
      private readonly List<string> keywords;


        //private Dictionary<string, List<>>();
      private Random random = new Random();

        public string Populate_responses {  get; set; }

        public string GetResponses()
        {
            var responses = new List<string>();

            responses.Add("Password");
            responses.Add("Scam");
            responses.Add("Malware");
            responses.Add("scam");
            responses.Add("Privacy");


            return "";
        }
    }
}
