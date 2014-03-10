using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icm.MediaLibrary.Domain
{
    public class FileCreateEventArgs : EventArgs
    {
        public FileCreateEventArgs(string fullPath)
        {
            this.FullPath = fullPath;
        }

        public string FullPath { get; set; }
    }
}
