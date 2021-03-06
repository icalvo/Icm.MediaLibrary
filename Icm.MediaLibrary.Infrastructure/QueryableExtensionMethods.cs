﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Icm.MediaLibrary.Infrastructure
{
    public static class QueryableExtensionMethods
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
}