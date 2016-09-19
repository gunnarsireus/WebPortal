using System;
using System.Web.Mvc;

namespace WebPortal.App_Start
{
    // Don't make empty strings in JSON to nulls
    // http://stackoverflow.com/questions/12734083/string-empty-converted-to-null-when-passing-json-object-to-mvc-controller

    public class EmptyStringModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            bindingContext.ModelMetadata.ConvertEmptyStringToNull = false;
            Binders = new ModelBinderDictionary() { DefaultBinder = this };
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
