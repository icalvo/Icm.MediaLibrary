using System.Collections.Generic;
using System.Linq;
using Icm.MediaLibrary.Domain;
using System.Linq.Expressions;
using System;

namespace Icm.MediaLibrary.Infrastructure
{
    public static class extensionmethods
    {
        public static IQueryable<T> OrderByFieldAscending<T>(this IQueryable<T> q, string sortField)
        {
            return q.OrderByField(sortField, true);
        }
        public static IQueryable<T> OrderByFieldDescending<T>(this IQueryable<T> q, string sortField)
        {
            return q.OrderByField(sortField, false);
        }
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool isAscending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = isAscending ? "OrderBy" : "OrderByDescending";
            Type[] types = { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }

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
