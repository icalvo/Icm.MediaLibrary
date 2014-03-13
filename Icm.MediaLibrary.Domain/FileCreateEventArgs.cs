using System;

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
