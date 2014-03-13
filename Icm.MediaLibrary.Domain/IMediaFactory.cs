namespace Icm.MediaLibrary.Domain
{
    public interface IMediaFactory
    {
        Media BuildFromFile(string filePath);
    }
}
