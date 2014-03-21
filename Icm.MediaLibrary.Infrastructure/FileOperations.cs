using System.IO;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class FileOperations : IFileOperations
    {
        public void Create(string filePath)
        {
            File.Create(filePath);
        }
    }
}
