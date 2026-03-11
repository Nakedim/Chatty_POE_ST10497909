
using System.Media;
using System;

namespace ChattyApp

{
    class Program
       

    {

 public static void PlaySound()
        {
            #pragma warning disable CA1416
            SoundPlayer player = new SoundPlayer(@"C:\Users\Sugar\Documents\Mystudies Second Year\PROG 2A\POE\Welcome Message.wav");
            player.Play();
            player.PlaySync();
          
            #pragma warning restore CA1416
        }


        public static void Main(string[] args)
        {
         PlaySound();
            
        }

       


    }
}
