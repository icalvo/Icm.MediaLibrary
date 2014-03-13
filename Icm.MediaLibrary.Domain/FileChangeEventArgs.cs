using System;

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
