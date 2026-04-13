using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
            Console.WriteLine("How are You " + user.Name + "? :");
         
           

            //Console.WriteLine("How are You " + user.Name + "? :");
            //Threading will help give the app chatting feel
            Console.WriteLine("Typing...");
            Thread.Sleep(2000);

            //ChatBot User Cyber Conversation

            bool isRunning = true;
            
            while (isRunning) 
            {
                String UserReply = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Green;
              
                String BotResponse = "I'm Fine";
                Console.WriteLine(BotResponse+ " What is your purpose " +user.Name +"?");

                //String UserResponse = Console.ReadLine();
                UserReply = Console.ReadLine();

                //We user IsNullOrWhiteSpace incase user enters backspaces/spaces/tabs
                if (string.IsNullOrWhiteSpace(UserReply))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Response cannot be empty: Enter your Response" +user.Name);
                    Console.WriteLine("What is your purpose " + user.Name + "?");
                    continue;
                }

                //We use StringComparison as it is case-insenstive
                if (UserReply.Contains("You", StringComparison.OrdinalIgnoreCase) || UserReply.Contains("Fine", StringComparison.OrdinalIgnoreCase))
                { 
                    
                    Console.WriteLine( "fine" + user.Name + " ?");
                    UserReply = Console.ReadLine();
                    Thread.Sleep(1000);
                    Console.WriteLine("Typing");

                }
                string LetsContinue;
                do
                {
                    Console.WriteLine("What can i ask You? " + "\n"
                        
                        + "1.Password safety " +"\n"
                        + "2.Safe Browsing" +"\n"
                        + "3.Phishing" + "\n"


                    );
                    
                    Console.WriteLine("Do you want to want to search another topic "+user.Name + "?\": Press y to continue or any key to Quit");
                     string topic = Console.ReadLine();
                LetsContinue = Console.ReadLine().Trim().ToLower();

                } while (LetsContinue == "y");
                 
                if (LetsContinue != "y")
                {
                    Console.WriteLine("Goodbye" +user.Name +" Have a nice day");
                    isRunning = false;
                }
                break;

            }



        }




    }
}
