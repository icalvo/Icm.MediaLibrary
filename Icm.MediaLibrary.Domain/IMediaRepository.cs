using System.Collections.Generic;

namespace Icm.MediaLibrary.Domain
{
    public interface IMediaRepository
    {
        bool ContainsFile(string filename);

        bool ContainsHash(string hash);

        void Add(Media media);

        IEnumerable<Video> GetVideos();
    }
}
