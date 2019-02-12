using MongoDB.Bson;
using System.Web.Mvc;

namespace Frame.MongoDB
{

    public class ObjectIdModelBinder : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

            if (bindingContext.ModelType != typeof(ObjectId) && bindingContext.ModelType != typeof(ObjectId?))
            {
                //return ObjectId.Empty;
                return null;
            }

            var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (val == null)
            {
                //return ObjectId.Empty;
                return null;
            }
            var value = val.AttemptedValue;
            if (value == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                //return ObjectId.Empty;
                return null;
            }
            ObjectId result;
            if (ObjectId.TryParse(value, out result))
            {
                return result;
            }
            //return ObjectId.Empty;
            return null;
        }

    }
}

