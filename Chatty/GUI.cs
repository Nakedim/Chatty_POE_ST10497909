using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace Chatty
{
    internal class GUI
    {
        private static String logo = @"  ____      _               ____                       _ _                _                
 / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _       / \   _ __  _ __  
| |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | |     / _ \ | '_ \| '_ \ 
| |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| |    / ___ \| |_) | |_) |
 \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, |   /_/   \_\ .__/| .__/ 
      |___/                                                  |___/            |_|   |_|    ";

        public string Name { get; set; }

        public  void PlaySound(User user)
        {
            Console.WriteLine(logo);
            SoundPlayer player = new SoundPlayer(@"C:\Users\Sugar\source\repos\Chatty\Chatty\Welcome Message.wav");
            //player.PlaySync();
            Console.WriteLine("\n");
            Console.WriteLine("Typing...");
            Thread.Sleep(2000);
            Console.WriteLine("What your is Name");
            user.Name = Console.ReadLine();


            Console.WriteLine("Typing...");
            Thread.Sleep(2000);
          






        }
    }
}
