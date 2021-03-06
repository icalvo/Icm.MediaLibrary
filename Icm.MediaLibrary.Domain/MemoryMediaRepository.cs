﻿using System.Collections.Generic;
using System.Linq;

namespace Icm.MediaLibrary.Domain
{
    public class MemoryMediaRepository : IMediaRepository
    {
        private readonly List<Media> store;

        public MemoryMediaRepository()
        {
            this.store = new List<Media>();
        }

        public bool ContainsFile(string fileName)
        {
            return this.store.Any(media => media.FileName == fileName);
        }

        public void Add(Media media)
        {
            this.store.Add(media);
        }

        public IEnumerable<Video> GetVideos()
        {
            return this.store.OfType<Video>();
        }
    }
}
