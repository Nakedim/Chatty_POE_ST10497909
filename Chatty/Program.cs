
using System.Media;
using System;
using Chatty;

namespace Chatty

{
    class Program
       

      {
     public static void Main(string[] args)
       {
            Intro intro = new Intro();
            BotData botData = new BotData();
            User user = new User();
            user.Name = "Nakedi";
  
            //intro.PlaySound(user);
            botData.getChatData(user);


        }


      }

    }

