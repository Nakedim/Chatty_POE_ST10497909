using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace CyberChat
{
    public class KeywordResponder
    {

       
        private Random random = new Random();

        //ignore case sensitivity when matching keywords

        public Dictionary<string, string> Cyberjagon()
        {
            return new Dictionary<string, string>()
            {
                ["Password"] = "A password is a secret word or phrase that is used to gain access to something, such as an account or device. It is important to use strong and unique passwords to protect your personal information and prevent unauthorized access.",
                ["Scam"] = "A scam is a fraudulent scheme or deception designed to trick people into giving away their money, personal information, or other valuable assets. Scams can take many forms, such as phishing emails, fake websites, or phone calls from impersonators. It is important to be cautious and skeptical of unsolicited offers or requests for sensitive information.",
                ["Malware"] = "Malware is a type of software that is designed to harm or exploit computer systems. It can take many forms, such as viruses, worms, trojans, ransomware, and spyware. Malware can be used to steal personal information, damage files, or take control of a computer. It is important to use antivirus software and keep it up to date to protect against malware threats.",
                ["Privacy"] = "Privacy refers to the right of individuals to keep their personal information and activities private and secure. It involves protecting sensitive data, such as financial information, health records, and online activities, from unauthorized access or disclosure. Privacy is an important aspect of digital security and is essential for maintaining trust in online interactions.",



            };
        
        }
        Dictionary<string,List<string>> responses = new Dictionary<string, List<string>>()
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
            }
        };

        public string GetResponse(string userInput)
        {
   
            var responses = Cyberjagon();
            foreach (var keyword in responses.Keys)
            {
                if (userInput.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return responses[keyword];
                }
            }
            return "I didn't understand that. Can you please rephrase?";
        }
    }

}