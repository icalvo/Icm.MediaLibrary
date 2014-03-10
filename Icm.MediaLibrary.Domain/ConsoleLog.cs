using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
