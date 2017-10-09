using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    public class JsonRPCModelBinder : System.Web.Mvc.IModelBinder
    {
        public JsonRPCRequest Request { get; private set; }

        public JsonRPCModelBinder(JsonRPCRequest req)
        {
            this.Request = req;
        }

        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            //Get access to the list of parameters in the calling action.
            var controllerDescriptor = new ReflectedControllerDescriptor(controllerContext.Controller.GetType()).FindAction(controllerContext, controllerContext.RouteData.GetRequiredString("action"));
            var parameters = controllerDescriptor.GetParameters().ToList();

            var parameterOfInterest = parameters.Single(p => bindingContext.ModelName == p.ParameterName);
            var paramNumber = parameters.IndexOf(parameterOfInterest);
                                                            
            var inputParameters = this.Request.Params;

            if (inputParameters == null || inputParameters.Length <= paramNumber)
                return null; //No data for this parameter, return null

            if(inputParameters[paramNumber] is string)
            {
                return inputParameters[paramNumber];
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject(inputParameters[paramNumber].ToString(), parameterOfInterest.ParameterType);
                }
                catch
                {
                    return null;
                }
            }           
        }
    }
}