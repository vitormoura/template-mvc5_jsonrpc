using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    public class JsonRPCModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            HttpContext httpContext = HttpContext.Current;
            RouteData routeData = httpContext.Request.RequestContext.RouteData;
            JsonRPCRequest jsonRequest = routeData.Values[JsonRPCRoute.ROUTE_DATA_RPC_REQUEST_KEY] as JsonRPCRequest;

            if (jsonRequest == null)
                return null;

            return new JsonRPCModelBinder(jsonRequest) as IModelBinder;
        }
    }
}