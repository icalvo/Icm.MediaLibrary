namespace Icm.MediaLibrary.Web.Models
{
    public abstract class BaseDataTablesModelBinder
    {
        protected object Bind()
        {
            var request = new DataTablesRequest();
            request.Challenge = GetRequired<int>("sEcho");
            request.DisplayedColumns = GetRequired<int>("iColumns");
            request.DisplayStart = GetRequired<int>("iDisplayStart");
            request.DisplayLength = GetRequired<int>("iDisplayLength");

            request.GlobalSearch = GetRequired<string>("sSearch");
            request.IsGlobalSearchRegex = GetRequired<bool>("bRegex");
            request.SortedColumns = GetRequired<int>("iSortingCols");

            for (int i = 0; i < request.DisplayedColumns; i++)
            {
                var column = new DataTablesColumnSearchRequest();
                column.ColumnIndex = i;
                column.IsSearchable = GetRequired<bool>("bSearchable_" + i);
                column.IsSortable = GetRequired<bool>("bSortable_" + i);
                column.Search = GetRequired<string>("sSearch_" + i);
                column.IsSearchRegex = GetRequired<bool>("bRegex_" + i);

                request.SearchColumns.Add(column);
            }

            for (int i = 0; i < request.SortedColumns; i++)
            {
                var column = new DataTablesColumnSortRequest();
                column.ColumnIndex = GetRequired<int>("iSortCol_" + i); ;
                column.Direction = GetRequired<string>("sSortDir_" + i) == "asc" ? SortingDirection.Ascending : SortingDirection.Descending;
                request.SortColumns.Add(column);
            }

            return request;
        }

        protected abstract T GetRequired<T>(string key);

        protected abstract T? GetOptional<T>(string key) where T : struct;
    }
}