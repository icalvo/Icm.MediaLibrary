using System;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Domain.Tests
{
    [TestClass]
    public class MediaWatcherTests
    {
        [TestMethod]
        public void GivenExistingFiles_GetsThemIntoRepository()
        {
            string audioFileName = "video.mp3";
            string videoFileName = "video.mp4";
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FakeFileSystem();
            IMediaRepository repository = new MemoryMediaRepository();
            IMediaFactory extractor = A.Fake<IMediaFactory>();
            A.CallTo(() => extractor.BuildFromFile(audioFileName)).Returns(new NonMedia());
            A.CallTo(() => extractor.BuildFromFile(videoFileName)).Returns(new Video("myhash", videoFileName, 340, TimeSpan.Zero, 640, 320));
            IFileSystemObserver observer = new FakeFileSystemObserver(fileSystem);
            MediaWatcher mediaWatcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            fileSystem.CreateFile(audioFileName);
            fileSystem.CreateFile(videoFileName);
            mediaWatcher.Synchronize(@"");

            Assert.IsTrue(repository.ContainsFile(videoFileName));
        }

        [TestMethod]
        public void GivenNoFile_WhenWatching_AndAddingFiles_GetsThemIntoRepository()
        {
            string audioFileName = "video.mp3";
            string videoFileName = "video.mp4";
            ILog log = new ConsoleLog();
            IFileSystem fileSystem = new FakeFileSystem();
            IMediaRepository repository = new MemoryMediaRepository();
            IMediaFactory extractor = A.Fake<IMediaFactory>();
            A.CallTo(() => extractor.BuildFromFile(audioFileName)).Returns(new NonMedia());
            A.CallTo(() => extractor.BuildFromFile(videoFileName)).Returns(new Video("myhash", videoFileName, 340, TimeSpan.Zero, 640, 320));
            IFileSystemObserver observer = new FakeFileSystemObserver(fileSystem);
            MediaWatcher watcher = new MediaWatcher(fileSystem, repository, extractor, observer, log);

            watcher.Watch();
            fileSystem.CreateFile(audioFileName);
            fileSystem.CreateFile(videoFileName);

            Assert.IsTrue(repository.ContainsFile(videoFileName));
        }
    }
}
