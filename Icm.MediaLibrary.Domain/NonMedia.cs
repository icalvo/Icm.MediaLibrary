namespace Icm.MediaLibrary.Domain
{
    public class NonMedia : Media
    {
        public override bool IsMedia()
        {
            return false;
        }
    }
}
