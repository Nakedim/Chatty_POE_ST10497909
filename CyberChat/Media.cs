using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Threading;

namespace Chatty
{
    internal class Media
    {
        public  String logo = @"  ____      _               ____                       _ _                _                
 / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _       / \   _ __  _ __  
| |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | |     / _ \ | '_ \| '_ \ 
| |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| |    / ___ \| |_) | |_) |
 \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, |   /_/   \_\ .__/| .__/ 
      |___/                                                  |___/            |_|   |_|    ";





        public static string greeting = @"XX    XX            XX         XX          XXXXXXXXX     XXXXXXXXX 
XX    XX  XXXXXX    XX         XX         XXXX    XXX   XX       XX
XX   XXX  XX        XX         XXX       X XX      XX   X         X
XXXXXXXX  XX        XX         XXX      XXXX       XXX  X         X
XXXXXXXX  XXXXXXX   XX         XXX      X X         XX  X         X
XX    XX  XXXXXX    XX         XXX      XXX        XXX   X        X
XX    XXX XX        XX         XX        XXX       XX    XXX      X
XX    XXX XXX       XXXXXXXXXX XX         XXXXXXXXXXX    XXXXXXXXXX
XX    XX  XXXXXX    XXXXXXXXXX XXXXXXXXX   XXXXXXXX       XXXXXXX  ";

        public string Name { get; set; }

        public  void PlaySound(User user)
        {
           
            SoundPlayer player = new SoundPlayer(@"C:\Users\Sugar\source\repos\Chatty\Chatty\Welcome Message.wav");
            player.PlaySync();

          
            
          
        }
    }
}
