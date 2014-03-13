using System.Collections.Generic;

namespace Icm.MediaLibrary.Domain
{
    public interface IFileSystem
    {
        void CreateFile(string filePath);

        IEnumerable<string> GetFilesRecursively(string directoryPath);

        void RegisterObserver(IFileSystemObserver observer);
    }
}
