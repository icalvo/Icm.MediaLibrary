using System.Collections.Generic;

namespace Icm.MediaLibrary.Domain
{
    public interface IDirectoryOperations
    {
        IEnumerable<string> GetFilesRecursively(string directoryPath);
    }
}