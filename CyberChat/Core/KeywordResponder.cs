namespace CyberChat.Core
{
    public class KeywordResponder
    {


        private Random random = new Random();

        Dictionary<string, List<string>> _responses = 
            
            new Dictionary<string, List<string>>()
        {
            ["Password"] = new List<string>()
            {
                "A password is a secret word or phrase that is used to gain access to something, such as an account or device. It is important to use strong and unique passwords to protect your personal information and prevent unauthorized access.",
                "A password is a string of characters that is used to authenticate a user's identity and grant access to a system or account. It is important to create strong passwords that are difficult for others to guess in order to protect your personal information and prevent unauthorized access."
            },
            ["Scam"] = new List<string>()
            {
                "A scam is a fraudulent scheme or deception designed to trick people into giving away their money, personal information, or other valuable assets. Scams can take many forms, such as phishing emails, fake websites, or phone calls from impersonators. It is important to be cautious and skeptical of unsolicited offers or requests for sensitive information.",
                "A scam is a dishonest scheme or fraud that is designed to deceive people and steal their money or personal information. Scams can take many forms, such as phishing emails, fake websites, or phone calls from impersonators. It is important to be vigilant and cautious when dealing with unsolicited offers or requests for sensitive information."
            },
            ["Malware"] = new List<string>()
            {
                "Malware is a type of software that is designed to harm or exploit computer systems. It can take many forms, such as viruses, worms, trojans, ransomware, and spyware. Malware can be used to steal personal information, damage files, or take control of a computer. It is important to use antivirus software and keep it up to date to protect against malware threats.",
                "Malware is malicious software that can infect your computer and cause harm. It can come in the form of viruses, worms, trojans, ransomware, and spyware. Malware can steal your personal information, damage your files, or even take control of your computer. To protect yourself from malware, it's important to use antivirus software and keep it updated regularly."
            },
            ["Privacy"] = new List<string>()
            {
                "Privacy refers to the right of individuals to keep their personal information and activities private and secure. It involves protecting sensitive data, such as financial information, health records, and online activities, from unauthorized access or disclosure. Privacy is an important aspect of digital security and is essential for"
            },

            ["Phishing"] = new List<string>()
            {
                "Phishing is a type of cyber attack that involves tricking individuals into providing sensitive information, such as passwords, credit card numbers, or social security numbers. Phishing attacks often come in the form of emails, text messages, or phone calls that appear to be from a legitimate source, such as a bank or a reputable company. It is important to be cautious and verify the authenticity of any communication before providing personal information."
            }

        };
        //greetings
        //The plan is to have a dictionary of greeting and response to use them in the logic rather using if statement/

        private static readonly Dictionary<string, List<string>> GreetingResponses = new()
    {
        { "hello", ["Hi there!", "Hello!", "Greetings!"] },
        { "how are you?", ["I'm doing well, thank you!", "I'm fine, how about you?", "I'm great, thanks for asking!"] },
        { "what's your name?", ["I'm a chatbot.", "I don't have a name.", "You can call me Chatbot."] }
    };

        public static Dictionary<string, List<string>> CyberKeywords = new()
        {
            ["What can i ask"] = new List<string>() { "Password", "Phishing", "Privacy", "Scam", "Malware" }
        };
        
     

        
      

        public string GetResponse(string UserInput)
        {
       
            var responses = _responses;
            if (string.IsNullOrWhiteSpace(UserInput))
            {
                return null;
            }
            foreach(var key in responses.Keys)
            {
                if(UserInput.IndexOf(key, StringComparison.OrdinalIgnoreCase ) >=0)
                {
                    var options = responses[key];
                    return options[random.Next(options.Count)];
                }
            }

            return "";
        }
        //get all keywords methods
        public List<string> getAllKeywords()
        {
            if(CyberKeywords == null)
            {
                return new List<string>();
            }
            return CyberKeywords.Keys.ToList();

        }
    }
   

}