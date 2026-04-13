using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Chatty
{
    internal class BotData
    {
        //Constructors 
        public BotData()
        {

        }

        public void ChatIntro(User user)

        {
          
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-----------INTRODUCTION--------------");
            Console.WriteLine("\n");
            Console.ResetColor();
            
         SetUserName(user);

            //Main app logic
            Console.WriteLine("Typing...");
            Thread.Sleep(2000);
            Console.WriteLine($"How are you {user.Name}?");
            String UserReply = GetValidInput(IsValidUserName,
                    "Invalid name. Letters only (2–30 characters).");
            Console.WriteLine($"You Said:{UserReply}" );

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

                Console.WriteLine(" What is your purpose " + user.Name + "?");
                UserReply = GetValidInput(IsValidUserName,
                    "Invalid name. Letters only (2–30 characters).");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Said: " + UserReply);




                string option;

                do
                {
                    Console.WriteLine("What can I ask you?\n"
                        + "1. Password safety\n"
                        + "2. Safe Browsing\n"
                        + "3. Phishing\n");

                    Console.WriteLine("Do you want to search another topic " + user.Name + "? (Press y to continue or any key to quit)");

                    option = Console.ReadLine();

                } while (option?.Equals("y", StringComparison.OrdinalIgnoreCase) == true);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Goodbye " + user.Name + " Have a nice day");
                Console.ResetColor();
                isRunning = false;


            }



        }

        //helper method to help handle invalid inputs
        private string GetValidInput(Func<string, bool> validator, string errorMessage)
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
        //helper method to help handle invalid inputs
        private bool ValidUserInputs(string input)
        {

            if (string.IsNullOrEmpty(input)) { 

                return false;
            input = input.Trim();

                //this will help user type meaningful message 
            if (input.Length < 2 || input.Length > 100) { 

                return false;
          
        }
 
    }
 return true;

}

        //this method will ensure dont not add  invalid characters and the username length is reasonable
        private bool IsValidUserName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            if (input.Length < 2 || input.Length > 30)
                return false;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }

            return true;
        }

        //method to get user to enter valid name
        private string GetValidUserName()
        {
            while (true)
            {
                Console.Write("Enter your name: ");
                string input = Console.ReadLine();

                if (IsValidUserName(input))
                    return input.Trim();
                //format error with red color to alert the user
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine("Invalid name. Use letters only (2–30 characters).");
                Console.ResetColor();
            }
        }
        private void SetUserName(User user)
        {
            Console.Write("Enter your name: ");

            user.Name = GetValidInput(
                IsValidUserName,
                "Invalid name. Letters only (2–30 characters)."
            );
        }

    }

}
