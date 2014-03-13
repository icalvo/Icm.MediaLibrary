using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.MediaLibrary.Domain
{
    public abstract class Media
    {
        protected Media()
        {
        }

        protected Media(string hash, string fileName, long size)
        {
            this.Hash = hash;
            this.FileName = fileName;
            this.Size = size;
        }

        public virtual bool IsMedia()
        {
            return true;
        }

        public string Hash { get; protected set; }

        public long Size { get; protected set; }

        public string FileName { get; protected set; }

        public string NormalizedTags { get; protected set; }

        public IEnumerable<string> Tags 
        { 
            get {
                return NormalizedTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(aString => aString.Trim());
            }
            protected set
            {
                NormalizedTags = string.Join(",", value);
            }
        }

        private static string NormalizeTag(string tag)
        {
            return tag.ToLowerInvariant().Trim().Replace(',', '_');
        }
        public void AddTag(string tag)
        {
            string normalizedTag = NormalizeTag(tag);
            if (!this.Tags.Contains(normalizedTag))
            {
                NormalizedTags += "," + normalizedTag;
            }
        }

        public void RemoveTag(string tag)
        {
            string normalizedTag = NormalizeTag(tag);
            var tags = this.Tags.ToList();
            if (tags.Contains(normalizedTag))
            {
                tags.Remove(normalizedTag);
                this.Tags = tags;
            }
        }
    }
}
