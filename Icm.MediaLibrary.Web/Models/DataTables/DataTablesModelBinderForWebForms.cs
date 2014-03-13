using System;

namespace Icm.MediaLibrary.Web.Models
{
    public class DataTablesModelBinderForWebForms : BaseDataTablesModelBinder, System.Web.ModelBinding.IModelBinder
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
}