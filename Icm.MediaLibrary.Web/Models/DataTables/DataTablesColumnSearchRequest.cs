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
}