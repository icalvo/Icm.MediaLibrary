using System.Collections.Generic;
using System.IO;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class FileSystem : IFileSystem
    {
        public FileSystem()
        {
            this.File = new FileOperations();
            this.Directory = new DirectoryOperations();  
        }

        public IFileOperations File { get; private set; }
        public IDirectoryOperations Directory { get; private set; }

        public void RegisterObserver(IFileSystemObserver observer)
        {
        }
    }
}
