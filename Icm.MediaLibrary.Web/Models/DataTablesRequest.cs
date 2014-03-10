using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Icm.MediaLibrary.Web.Models
{
    //public static class extensionmethods
    //{
    //    public static IQueryable<T> OrderBy<T>(this IQueryable<T> q, string SortField)
    //    {
    //        var param = Expression.Parameter(typeof(T), "p");
    //        var prop = Expression.Property(param, SortField);
    //        var exp = Expression.Lambda(prop, param);
    //        string method = "OrderBy";
    //        Type[] types = new Type[] { q.ElementType, exp.Body.Type };
    //        var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
    //        return q.Provider.CreateQuery<T>(mce);
    //    }

    //    public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> q, string SortField)
    //    {
    //        var param = Expression.Parameter(typeof(T), "p");
    //        var prop = Expression.Property(param, SortField);
    //        var exp = Expression.Lambda(prop, param);
    //        string method = "OrderByDescending";
    //        Type[] types = new Type[] { q.ElementType, exp.Body.Type };
    //        var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
    //        return q.Provider.CreateQuery<T>(mce);
    //    }

    //}

    public class DataTablesRequest
    {
        public DataTablesRequest()
        {
            this.SearchColumns = new List<DataTablesColumnSearchRequest>();
            this.SortColumns = new List<DataTablesColumnSortRequest>();
        }

        public int Challenge { get; set; }
        public int DisplayedColumns { get; set; }
        public int? DisplayStart { get; set; }
        public int? DisplayLength { get; set; }
        public string GlobalSearch { get; set; }
        public bool IsGlobalSearchRegex { get; set; }
        public int SortedColumns { get; set; }

        public IList<DataTablesColumnSearchRequest> SearchColumns { get; set; }

        public IList<DataTablesColumnSortRequest> SortColumns { get; set; }

        public dynamic Response<T>(IQueryable<T> items, params IField<T>[] fields)
        {
            var result = items;

            if (!string.IsNullOrEmpty(this.GlobalSearch))
            {
                foreach (var column in this.SearchColumns)
                {
                    if (column.IsSearchable)
                    {
                        result = fields[column.ColumnIndex].Search(result, this.GlobalSearch);
                    }
                }
            }

            foreach (var column in this.SearchColumns)
            {
                if (column.IsSearchable)
                {
                    result = fields[column.ColumnIndex].Search(result, column.Search);
                }
            }

            foreach (var column in this.SortColumns)
            {
                result = fields[column.ColumnIndex].Sort(result);
            }

            if (DisplayStart.HasValue)
            {
                result = result.Skip(DisplayStart.Value);
            }

            if (DisplayLength.HasValue)
            {
                result = result.Take(DisplayLength.Value);
            }

            var resultData = result.ToList().Select(item => {
                var row = new List<string>();
                foreach (var column in fields)
                {
                    row.Add(column.GetString(item));
                }

                return row;
            });

            return new
            {
                sEcho = Challenge,
                iTotalRecords = items.Count(),
                iTotalDisplayRecords = result.Count(),
                aaData = resultData,
            };
        }

    }
}