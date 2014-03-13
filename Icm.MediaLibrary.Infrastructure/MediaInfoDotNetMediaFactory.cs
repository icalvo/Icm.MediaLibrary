using System;
using System.IO;
using System.Security.Cryptography;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class MediaInfoDotNetMediaFactory : IMediaFactory
    {
        public Media BuildFromFile(string filePath)
        {
            Media result;
            var info = new MediaInfoDotNet.MediaFile(filePath);
            SHA1 sha1Encoder = new SHA1CryptoServiceProvider();

            FileStream stream = File.OpenRead(filePath);
            byte[] byteHash = sha1Encoder.ComputeHash(stream);
            stream.Close();
            string hash = System.BitConverter.ToString(byteHash);

            if (info.Video.Count > 0)
            {
                result = new Video(
                    hash,
                    filePath, 
                    info.size,
                    TimeSpan.FromMilliseconds(info.Video[0].duration),
                    info.Video[0].width,
                    info.Video[0].height);
            }
            else
            {
                result = new NonMedia();
            }
            return result;
        }
    }
}
