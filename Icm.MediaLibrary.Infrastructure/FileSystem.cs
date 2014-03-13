using System.Collections.Generic;
using System.IO;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class FileSystem : IFileSystem
    {
        public void CreateFile(string filePath)
        {
            File.Create(filePath);
        }

        public IEnumerable<string> GetFilesRecursively(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
        }

        public void RegisterObserver(IFileSystemObserver observer)
        {
        }
    }
}
