
using System.Media;
using System;

namespace ChattyApp

{
    class Program
       

    {

 public static void PlaySound()
        {
            #pragma warning disable CA1416
            SoundPlayer player = new SoundPlayer(@"C:\Users\Student\source\repos\Chatty_POE_ST10497909\Chatty\Welcome.wav");
            player.Play();
            player.PlaySync();
          
            #pragma warning restore CA1416
        }

public static void Greeting(String userAnswer)
        {
            //Bitmap image = new Bitmap();



            String logo = @"  ____      _               ____                       _ _                _                
 / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _       / \   _ __  _ __  
| |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | |     / _ \ | '_ \| '_ \ 
| |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| |    / ___ \| |_) | |_) |
 \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, |   /_/   \_\ .__/| .__/ 
      |___/                                                  |___/            |_|   |_|    ";

                                                                                 

            Console.WriteLine(logo);
            //Paused this method to speeding testing
            //PlaySound();
            String Welcome = @"  ____               _   _                   _         __   __          _ 
 / ___|_ __ ___  ___| |_(_)_ __   __ _ ___  | |_ ___   \ \ / /__  _   _| |
| |  _| '__/ _ \/ _ \ __| | '_ \ / _` / __| | __/ _ \   \ V / _ \| | | | |
| |_| | | |  __/  __/ |_| | | | | (_| \__ \ | || (_) |   | | (_) | |_| |_|
 \____|_|  \___|\___|\__|_|_| |_|\__, |___/  \__\___/    |_|\___/ \__,_(_)
                                 |___/                                    ";


            /*   Console.WriteLine("\n");
               Console.WriteLine("What your is Name");
               String name = Console.ReadLine();
               Console.WriteLine(Welcome +  name +" Welcome to the App");
               Console.WriteLine("\n");
               Console.WriteLine("How are You " + name);



              Console.WriteLine("What is your purpose?");
               String question_1 = Console.ReadLine();
               Console.WriteLine();*/

            
           
            //Storing answering
            String answer_1 = "Password safety include making sure the password is made up of characters, numeric and letters";
            String answer_2 = "Do not click onto unknowing links and email";
            String answer_3 = "Safe browsing including \n 1: Make sure website is secured with https \n 2: never let browser remember password if using public" +
                "\n 3: Use good password ";
            //Storing list of topics 
            String[] topics = { "Password Safety", "Phishing","Safe Browsing" };

            Console.WriteLine("What can i ask you About?");
            String question_2 = Console.ReadLine();

           

            //check if input is empty
            while (string.IsNullOrEmpty(question_2)) 
            {
                Console.WriteLine("re-enter your response!");
                question_2 = Console.ReadLine();

            }
            Console.WriteLine("Choose the following topics:\n1:Password Safety\n2:Phishing\n3:Safe Browsing");
            string response = Console.ReadLine();
            int res = Int32.Parse(response);

            switch (res)
            {
                    case 1:
                    Console.WriteLine("Chatty Bot: " +answer_1);
                    break;
                    case 2:
                    Console.WriteLine("Chatty Bot: " + answer_2);
                    break;
                    case 3:
                    Console.WriteLine("Chatty Bot: " + answer_3);
                    break;
                default:
                    Console.WriteLine("Invalid response: Enter number between 1 - 3");
                    break;
                    

            }
            Console.WriteLine("is there anything i can assist you with");
            switch (res)
            {
                case 1:
                Console.WriteLine("What is your purpose");
                    break;
            }
            
            String question_3 = Console.ReadLine();
            while (string.IsNullOrEmpty(question_3))
            {
                Console.WriteLine("re-enter your response!");
                question_3 = Console.ReadLine();

            }
















        }

    
       






        public static void Main(string[] args)
        {
        Greeting("");
            
        }

       


    }
}
