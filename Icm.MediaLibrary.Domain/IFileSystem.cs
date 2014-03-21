using System.Collections.Generic;

namespace Icm.MediaLibrary.Domain
{
    public interface IFileSystem
    {
        IFileOperations File { get; }
        IDirectoryOperations Directory { get; }

        void RegisterObserver(IFileSystemObserver observer);
    }
}
