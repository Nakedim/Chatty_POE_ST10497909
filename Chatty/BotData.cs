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


            Console.Write("How are You " + user.Name + "? :");
            String UserReply = Console.ReadLine();

            Console.WriteLine("Typing...");
            Thread.Sleep(2000);

            //ChatBot User Cyber Conversation

            Console.WriteLine("How can we assist you " + user.Name + "?");
            
            

            bool isRunning = true;
            
            while (isRunning) 
            {
                String UserResponse = Console.ReadLine();

                //We user IsNullOrWhiteSpace incase user enters backspaces/spaces/tabs
                if (string.IsNullOrWhiteSpace(UserResponse))
                {
                    Console.WriteLine(" Response cannot be empty: Enter your Response");
                   continue;
                   
                   
                }

                if (UserResponse.Contains("password", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Password must be a mix of alphaNumeric character, new share your password ");
                    UserResponse = Console.ReadLine();
                do
                {
                    Console.WriteLine("Do you want to want to search another topic: Press Y to continue or any key to Quit");

                } while (Console.ReadLine().Trim().ToLower() == "y");

          
                }
                
                if (UserResponse.Contains("phishing", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Do not Open unknown email links");
                    UserResponse = Console.ReadLine();
                    continue;

                }
                break;


                
            }



            

            Console.WriteLine("Whats your purpose");
            Console.WriteLine("What can i ask you about");

            //Password Safety, Phishing, Safe browsing


        }


    }
}
