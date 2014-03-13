using System;

namespace Icm.MediaLibrary.Domain.Tests
{
    public class FakeFileSystemObserver : IFileSystemObserver
    {
        private readonly IFileSystem fileSystemToWatch;
        public FakeFileSystemObserver(IFileSystem fileSystemToWatch)
        {
            this.fileSystemToWatch = fileSystemToWatch;
        }

        public void Watch()
        {
            this.fileSystemToWatch.RegisterObserver(this);
        }

        public event EventHandler<FileChangeEventArgs> Changed;

        public event EventHandler<FileCreateEventArgs> Created;

        public event EventHandler<FileDeleteEventArgs> Deleted;

        public event EventHandler<FileCreateEventArgs> Error;

        public event EventHandler<FileRenameEventArgs> Renamed;

        void IFileSystemObserver.NotifyCreated(string filePath)
        {
            Created(this, new FileCreateEventArgs(filePath));
        }
    }
}
