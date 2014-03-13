using Icm.MediaLibrary.Domain;
using Icm.MediaLibrary.Infrastructure;

namespace Icm.MediaLibrary.Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FileSystem();
            IMediaRepository repository = new DbMediaRepository(new EntityFrameworkSexContext());
            IMediaFactory extractor = new MediaInfoDotNetMediaFactory();
            IFileSystemObserver observer = new FileSystemObserver(@"\\Etrayz\public\Video\Otros");
            MediaWatcher watcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            watcher.Synchronize(@"\\Etrayz\public\Video\Otros");
            watcher.Watch();
        }
    }
}
