using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Icm.MediaLibrary.Domain
{
    public class FileChangeEventArgs : EventArgs
    {
        public FileChangeEventArgs(string fullPath)
        {
            this.FullPath = fullPath;
        }

        public string FullPath { get; set; }
    }
}
