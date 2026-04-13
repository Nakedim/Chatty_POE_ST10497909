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
        private string GetValidInput()
        {
            while (true)

            {
                string input = Console.ReadLine();
                if (ValidUserInputs(input))
                {
                    return input.Trim();
                }
               Console.WriteLine("Enter valid input");
            }
            

        }
        private bool ValidUserInputs(string input)
        {

            if (string.IsNullOrEmpty(input)) { 

                return false;
            input = input.Trim();

            if (input.Length < 2 || input.Length > 100) { 

                return false;
          
        }
 
    }
 return true;

}

        public void getChatData(User user) 
        {

            
            //Main app logic
            Console.WriteLine("How are You " + user.Name + "? :");
            String UserReply = GetValidInput();
            //Console.WriteLine("How are You " + user.Name + "? :");
            //Threading will help give the app chatting feel
            Console.WriteLine("Typing...");
            Thread.Sleep(2000);
            Console.WriteLine("Im glad to hear that, im also good");
            Thread.Sleep(2000);
          

            //ChatBot User Cyber Conversation




            bool isRunning = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------MAIN CHAT CONVERSATIONS--------------");
            while (isRunning)
                
            {   
               
                Console.WriteLine(" What is your purpose " +user.Name +"?");
                UserReply = GetValidInput();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Said: " + UserReply);


                //We use StringComparison as it is case-insenstive
                //if (UserReply.Contains("You", StringComparison.OrdinalIgnoreCase) || UserReply.Contains("Fine", StringComparison.OrdinalIgnoreCase))
                //{ 
                //    Console.WriteLine("Im glad to hear that, im also good");
                //    Thread.Sleep(2000);
                //    Console.WriteLine("Typing");
                //    continue;
                //}

                String Continue;
                do
                {
                    Console.WriteLine("What can i ask You? " + "\n"
                        
                        + "1.Password safety " +"\n"
                        + "2.Safe Browsing" +"\n"
                        + "3.Phishing" + "\n"


                    );
                    
                    Console.WriteLine("Do you want to want to search another topic "+user.Name + "?\": Press y to continue or any key to Quit");
                    
                  Continue = Console.ReadLine();

                } while (Continue.Contains("y"));
                 
                if (Continue != "y")
                {
                    Console.WriteLine("Goodbye " +user.Name +" Have a nice day");
                    isRunning = false;
                }
             

            }



        }




    }
}
