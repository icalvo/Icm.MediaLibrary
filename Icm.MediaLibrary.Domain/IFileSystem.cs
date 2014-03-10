using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Icm.MediaLibrary.Domain
{
    public interface IFileSystem
    {
        void CreateFile(string filePath);

        IEnumerable<string> GetFilesRecursively(string directoryPath);

        void RegisterObserver(IFileSystemObserver observer);
    }
}
