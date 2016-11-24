using System.Collections.Generic;
using System.Linq;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class DbMediaRepository : IMediaRepository
    {
        private readonly EntityFrameworkSexContext context;

        public DbMediaRepository(EntityFrameworkSexContext context)
        {
            this.context = context;
        }

        public bool ContainsFile(string filePath)
        {
            return this.context.Media.Any(media => media.FileName == filePath);
        }

        public void Add(Media media)
        {
            if (!this.ContainsFile(media.FileName))
            {
                this.context.Media.Add(media);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Video> GetVideos()
        {
            return this.context.Media.OfType<Video>();
        }
    }
}
