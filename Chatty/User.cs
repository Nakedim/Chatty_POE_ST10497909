using System;
using System.Collections.Generic;
using System.Text;

namespace Chatty
{
    internal class User
    {
        public string Name { get; set; }
    }

    //We use StringComparison as it is case -insenstive
    //if (UserReply.Contains("Fine", StringComparison.OrdinalIgnoreCase))
    //    {
    //        Console.WriteLine("Im glad to hear that, im also good");
    //        Thread.Sleep(1500);
    //        Console.WriteLine("Typing");

    //    }
    //if (UserReply.Contains("You", StringComparison.OrdinalIgnoreCase))
    //{
    //    Console.WriteLine("I'm also good");
    //    Thread.Sleep(1500);
    //    Console.WriteLine("Typing");
    //}
}
