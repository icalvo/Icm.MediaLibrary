using Icm.MediaLibrary.Domain;
using Icm.MediaLibrary.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icm.MediaLibrary.Integration.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FileSystem();
            IMediaRepository repository = new MemoryMediaRepository();
            IMediaFactory extractor = new MediaInfoDotNetMediaFactory();
            IFileSystemObserver observer = new FileSystemObserver(@"\\Etrayz\public\Video\Otros");
            MediaWatcher watcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            watcher.Synchronize(@"\\Etrayz\public\Video\Otros");

            Assert.IsTrue(repository.ContainsFile(""));
        }
    }
}
