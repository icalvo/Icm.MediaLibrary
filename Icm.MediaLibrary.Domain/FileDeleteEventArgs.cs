using System;

namespace Icm.MediaLibrary.Domain
{
    public class FileDeleteEventArgs : EventArgs
    {
        public FileDeleteEventArgs(string fullPath)
        {
            this.FullPath = fullPath;
        }

        public string FullPath { get; set; }
    }
}
