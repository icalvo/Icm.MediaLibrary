using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class FileSystemObserver : IFileSystemObserver
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        public FileSystemObserver(string directoryPath)
        {
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath);
        }

        public void Watch()
        {
            this.fileSystemWatcher.Created += (obj, e) => Created(obj, new FileCreateEventArgs(e.FullPath));
            this.fileSystemWatcher.EnableRaisingEvents = true;
        }

        public event EventHandler<FileChangeEventArgs> Changed;

        public event EventHandler<FileCreateEventArgs> Created;

        public event EventHandler<FileDeleteEventArgs> Deleted;

        public event EventHandler<FileCreateEventArgs> Error;

        public event EventHandler<FileRenameEventArgs> Renamed;

        public void NotifyCreated(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
