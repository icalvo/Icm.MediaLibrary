using System.Collections.Generic;
using System.Linq;

namespace Icm.MediaLibrary.Domain.Tests
{
    public class FakeFileSystem : IFileSystem
    {
        private readonly List<string> files;
        private readonly List<IFileSystemObserver> observers;

        public FakeFileSystem()
        {
            this.files = new List<string>();
            this.observers = new List<IFileSystemObserver>();
        }

        public void CreateFile(string filePath)
        {
            this.files.Add(filePath);
            foreach (var observer in observers)
            {
                observer.NotifyCreated(filePath);
            }
        }

        public IEnumerable<string> GetFilesRecursively(string directoryPath)
        {
            return this.files.Where(file => file.StartsWith(directoryPath));
        }

        public void RegisterObserver(IFileSystemObserver observer)
        {
            observers.Add(observer);
        }
    }
}
