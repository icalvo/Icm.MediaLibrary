using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
