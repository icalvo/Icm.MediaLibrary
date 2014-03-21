using System;

namespace Icm.MediaLibrary.Domain
{
    public class MediaWatcher
    {
        private readonly IMediaRepository repository;
        private readonly IMediaFactory informationExtractor;
        private readonly IFileSystem fileSystem;
        private readonly IFileSystemObserver fileWatcher;
        private readonly ILog log;

        public MediaWatcher(IFileSystem fileSystem, IMediaRepository repository, IMediaFactory informationExtractor, IFileSystemObserver fileWatcher, ILog log)
        {
            this.fileSystem = fileSystem;
            this.repository = repository;
            this.informationExtractor = informationExtractor;
            this.fileWatcher = fileWatcher;
            this.fileWatcher.Created += fileWatcher_Created;
            this.fileWatcher.Changed += fileWatcher_Changed;
            this.fileWatcher.Deleted += fileWatcher_Deleted;
            this.fileWatcher.Renamed += fileWatcher_Renamed;
            this.log = log;
        }

        public void Synchronize(string rootPath)
        {
            foreach (string filePath in this.fileSystem.Directory.GetFilesRecursively(rootPath))
            {
                this.AddFileIfMedia(filePath);
            }
        }

        private void AddFileIfMedia(string filePath)
        {
            if (this.repository.ContainsFile(filePath))
            {
                this.log.Info(filePath + " not added because it is already added.");
            }
            else
            {
                Media media = informationExtractor.BuildFromFile(filePath);
                if (media.IsMedia())
                {
                    this.repository.Add(media);
                    this.log.Info(filePath + " added.");
                }
                else
                {
                    this.log.Info(filePath + " not added because it is not a media file.");
                }
            }

        }

        void fileWatcher_Renamed(object sender, FileRenameEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fileWatcher_Deleted(object sender, FileDeleteEventArgs e)
        {
            throw new NotImplementedException();
        }

        void fileWatcher_Created(object sender, FileCreateEventArgs e)
        {
            AddFileIfMedia(e.FullPath);
        }

        void fileWatcher_Changed(object sender, FileChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Watch()
        {
            fileWatcher.Watch();
        }
    }
}
