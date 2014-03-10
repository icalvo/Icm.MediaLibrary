using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Icm.MediaLibrary.Web.Models
{
    public enum SortingDirection {
        None = 0,
        Ascending,
        Descending
    }

    public class DataTablesColumnSearchRequest
    {
        public int ColumnIndex { get; set; }
        public bool IsSearchable { get; set; }
        public string Search { get; set; }
        public bool IsSearchRegex { get; set; }
        public bool IsSortable { get; set; }
        public string Property { get; set; }
    }

    public class DataTablesColumnSortRequest
    {
        public int ColumnIndex { get; set; }
        public SortingDirection Direction { get; set; }
    }
}