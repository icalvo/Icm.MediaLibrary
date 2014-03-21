using System.Collections.Generic;
using System.IO;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    internal class DirectoryOperations : IDirectoryOperations
    {
        public IEnumerable<string> GetFilesRecursively(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
        }
    }
}