using System;
using System.Collections.Generic;
using System.Text;

namespace Chatty
{
    internal class BotData
    {
        public BotData() 
        {

        }
        public void getChatData(User user) 
        {

            //Main app logic
            Console.Write("How are You " + user.Name + "? :");
           

            //Threading will help give the app chatting feel
            Console.WriteLine("Typing...");
            Thread.Sleep(2000);

            //ChatBot User Cyber Conversation

       
            bool isRunning = true;
            
            while (isRunning) 
            {
                //String UserResponse = Console.ReadLine();
                String UserReply = Console.ReadLine();

                //We user IsNullOrWhiteSpace incase user enters backspaces/spaces/tabs
             
                if (string.IsNullOrWhiteSpace(UserReply))
                {
                    Console.WriteLine(" Response cannot be empty: Enter your Response");
                    continue;
                }

                //We use StringComparison as it is case-insenstive
                if (UserReply.Contains("You", StringComparison.OrdinalIgnoreCase) || UserReply.Contains("Fine", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("I'm Fine, What is your Purpose " + user.Name + "?");
                    UserReply = Console.ReadLine();
                    Thread.Sleep(1000);
                    Console.WriteLine("Typing");

                }
                string LetsContinue;
                do
                {
                    Console.WriteLine("Enter the topic you want to search:");
                    string topic = Console.ReadLine();
                    Console.WriteLine("Do you want to want to search another topic: Press y to continue or any key to Quit");
                    LetsContinue = Console.ReadLine().Trim().ToLower();

                } while (LetsContinue == "y");
                if (LetsContinue != null )
                    LetsContinue = Console.ReadLine().Trim().ToLower();
                {
                    Console.WriteLine();
                }
                if (UserReply.Contains("phishing", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Do not Open unknown email links");
                    UserReply = Console.ReadLine();
                    continue;
                }
                break;

            }


           

            //Password Safety, Phishing, Safe browsing


        }




    }
}
