
using System.Media;
using System;
using Chatty;

namespace Chatty

{
    class Program
       


      {
        public static void Main(string[] args)
        {
           
            BotData botData = new BotData();
            Media userInterFace = new Media();
            User user = new User();
            user.Name = "Nakedi";

            userInterFace.Name = "Chatty";
            userInterFace.PlaySound(user);
            botData.ChatIntro(user);


        }

 

      }

    }

