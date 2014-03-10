using System.Linq;

namespace Icm.MediaLibrary.Web.Models
{
    public interface IField<T>
    {
        string GetString(T item);
        IQueryable<T> Search(IQueryable<T> items, string searchString);
        IQueryable<T> Sort(IQueryable<T> items);
    }
}