using System;

namespace Icm.MediaLibrary.Web.Models
{
    public class DataTablesModelBinderForWebApi : BaseDataTablesModelBinder, System.Web.Http.ModelBinding.IModelBinder
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
}