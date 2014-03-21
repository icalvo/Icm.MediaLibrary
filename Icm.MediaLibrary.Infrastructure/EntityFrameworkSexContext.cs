using System.Data.Entity;
using Icm.MediaLibrary.Domain;

namespace Icm.MediaLibrary.Infrastructure
{
    public class EntityFrameworkSexContext : DbContext
    {
        public virtual IDbSet<Media> Media { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .HasKey(media => media.FileName);
        }
    }
}
