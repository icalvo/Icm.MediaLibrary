using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Icm.MediaLibrary.Domain.Tests
{
    [TestClass]
    public class MediaWatcherTests
    {
        [TestMethod]
        public void GivenExistingFiles_GetsThemIntoRepository()
        {
            const string audioFileName = "video.mp3";
            const string videoFileName = "video.mp4";
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FakeFileSystem();
            IMediaRepository repository = new MemoryMediaRepository();
            IMediaFactory extractor = A.Fake<IMediaFactory>();
            A.CallTo(() => extractor.BuildFromFile(audioFileName)).Returns(new NonMedia());
            A.CallTo(() => extractor.BuildFromFile(videoFileName)).Returns(new Video(videoFileName, 340, TimeSpan.Zero, 640, 320));
            IFileSystemObserver observer = new FakeFileSystemObserver(fileSystem);
            MediaWatcher mediaWatcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            fileSystem.File.Create(audioFileName);
            fileSystem.File.Create(videoFileName);
            mediaWatcher.Synchronize(@"");

            Assert.IsTrue(repository.ContainsFile(videoFileName));
        }

        [TestMethod]
        public void GivenNoFile_WhenWatching_AndAddingFiles_GetsThemIntoRepository()
        {
            const string audioFileName = "video.mp3";
            const string videoFileName = "video.mp4";
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FakeFileSystem();
            IMediaRepository repository = new MemoryMediaRepository();
            IMediaFactory extractor = A.Fake<IMediaFactory>();
            A.CallTo(() => extractor.BuildFromFile(audioFileName)).Returns(new NonMedia());
            A.CallTo(() => extractor.BuildFromFile(videoFileName)).Returns(new Video(videoFileName, 340, TimeSpan.Zero, 640, 320));
            IFileSystemObserver observer = new FakeFileSystemObserver(fileSystem);
            MediaWatcher watcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            watcher.Watch();
            fileSystem.File.Create(audioFileName);
            fileSystem.File.Create(videoFileName);

            Assert.IsTrue(repository.ContainsFile(videoFileName));
        }
    }
}
