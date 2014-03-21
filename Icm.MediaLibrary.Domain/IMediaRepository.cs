using System.Collections.Generic;

namespace Icm.MediaLibrary.Domain
{
    public interface IMediaRepository
    {
        bool ContainsFile(string filename);

        void Add(Media media);

        IEnumerable<Video> GetVideos();
    }
}
