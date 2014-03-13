using System;

namespace Icm.MediaLibrary.Domain
{
    public class FileRenameEventArgs : EventArgs
    {
        public FileRenameEventArgs(string fullPath)
        {
            this.FullPath = fullPath;
        }

        public string FullPath { get; set; }
    }
}
