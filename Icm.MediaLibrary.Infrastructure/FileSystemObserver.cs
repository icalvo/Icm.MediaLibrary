using System;
using System.IO;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class FileSystemObserver : IFileSystemObserver
    {
        private readonly FileSystemWatcher fileSystemWatcher;
        public FileSystemObserver(string directoryPath)
        {
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.Attributes };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.CreationTime };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.DirectoryName };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.FileName };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.LastAccess };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.LastWrite };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.Security };
            this.fileSystemWatcher = new FileSystemWatcher(directoryPath) { NotifyFilter = NotifyFilters.Size };
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
