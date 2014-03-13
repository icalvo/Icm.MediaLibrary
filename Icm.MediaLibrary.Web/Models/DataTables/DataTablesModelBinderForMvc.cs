namespace Icm.MediaLibrary.Web.Models
{
    public class DataTablesModelBinderForMvc : BaseDataTablesModelBinder, System.Web.Mvc.IModelBinder
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