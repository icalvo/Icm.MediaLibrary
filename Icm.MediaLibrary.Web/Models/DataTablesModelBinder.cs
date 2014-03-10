using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Icm.MediaLibrary.Web.Controllers;

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


    public class DataTablesModelBinder1 : BaseDataTablesModelBinder, System.Web.ModelBinding.IModelBinder
    {
        private System.Web.ModelBinding.ModelBindingContext bindingContext;

        public bool BindModel(System.Web.ModelBinding.ModelBindingExecutionContext modelBindingExecutionContext, System.Web.ModelBinding.ModelBindingContext bindingContext)
        {

            // can't be null
            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }
            // can't be null
            if (modelBindingExecutionContext == null)
            {
                throw new ArgumentNullException("modelBindingExecutionContext");
            }
            this.bindingContext = bindingContext;

            Bind();

            return true;
        }

        // get processing value
        protected override T? GetOptional<T>(string key)
        {
            System.Web.ModelBinding.ValueProviderResult valueResult;
            valueResult = this.bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
            {
                return null;
            }
            else
            {
                return (T?)valueResult.ConvertTo(typeof(T));
            }
        }

        protected override T GetRequired<T>(string key)
        {
            throw new NotImplementedException();
        }
    }

    public class DataTablesModelBinder2 : BaseDataTablesModelBinder, System.Web.Http.ModelBinding.IModelBinder
    {
        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }

        // get processing value
        private T? GetValue<T>(System.Web.Http.ModelBinding.ModelBindingContext bindingContext, string key) where T : struct
        {
            System.Web.Http.ValueProviders.ValueProviderResult valueResult;
            valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
            {
                return null;
            }
            else
            {
                return (T?)valueResult.ConvertTo(typeof(T));
            }
        }

        protected override T GetRequired<T>(string key)
        {
            throw new NotImplementedException();
        }

        protected override T? GetOptional<T>(string key)
        {
            throw new NotImplementedException();
        }
    }

    public class DataTablesModelBinder3 : BaseDataTablesModelBinder, System.Web.Mvc.IModelBinder
    {
        private System.Web.Mvc.ModelBindingContext bindingContext;

        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            this.bindingContext = bindingContext;

            return this.Bind();
        }

        // get processing value
        protected override T GetRequired<T>(string key)
        {
            System.Web.Mvc.ValueProviderResult valueResult;
            valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
            {
                this.bindingContext.ModelState.AddModelError(key, key + " cannot be null");
                return default(T);
            }
            else
            {
                return (T)valueResult.ConvertTo(typeof(T));
            }
        }

        // get processing value
        protected override  T? GetOptional<T>(string key)
        {
            System.Web.Mvc.ValueProviderResult valueResult;
            valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
            {
                return null;
            }
            else
            {
                return (T?)valueResult.ConvertTo(typeof(T));
            }
        }
    }


}