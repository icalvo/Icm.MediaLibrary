using System;

namespace Icm.MediaLibrary.Domain
{
    public class ConsoleLog : ILog
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
