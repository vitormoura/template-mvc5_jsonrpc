using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    public class JsonRPCRoute : Route
    {
        public static string ROUTE_DATA_RPC_REQUEST_KEY = "rpc:request";

        public JsonRPCRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) { }
        public JsonRPCRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler) { }
        public JsonRPCRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler) { }
        public JsonRPCRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData routeData = base.GetRouteData(httpContext);
            JsonRPCRequest jsonRequest = null;
                        
            if (routeData == null)
                return null;
                        
            if (string.Compare(httpContext.Request.ContentType, @"application/json", StringComparison.OrdinalIgnoreCase) != 0)
                return null;

            if (httpContext.Request.InputStream == null || httpContext.Request.InputStream.Length == 0)
                return null;

            using (StreamReader reader = new StreamReader(httpContext.Request.InputStream))
            {
                jsonRequest = JsonConvert.DeserializeObject<JsonRPCRequest>(reader.ReadToEnd());

                if (String.IsNullOrEmpty(jsonRequest.ID))
                    throw new HttpException((int)HttpStatusCode.BadRequest, @"The 'id' of request is invalid");

                if (String.IsNullOrEmpty(jsonRequest.Method))
                    throw new HttpException((int)HttpStatusCode.BadRequest, @"The 'method' name is invalid.");

                if (!Regex.IsMatch(jsonRequest.Method, "^[A-Za-z0-9_:/]+.[A-Za-z0-9_:/]+$"))
                    throw new HttpException((int)HttpStatusCode.BadRequest, @"The 'method' name is in the incorrect format.");

                var methodNameParts = jsonRequest.Method.Split('.');

                routeData.Values["controller"] = methodNameParts[0];
                routeData.Values["action"] = methodNameParts[1];
                

                routeData.Values[ROUTE_DATA_RPC_REQUEST_KEY] = jsonRequest;
            }

            return routeData;
        }
    }

    public static class JsonRPCRouteExtensions
    {
        public static JsonRPCRoute MapJsonRpcRoute(this RouteCollection routes, string name, string url)
        {
            if (routes == null) throw new ArgumentNullException("routes");
            if (url == null) throw new ArgumentNullException("url");

            JsonRPCRoute route = new JsonRPCRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(),
                DataTokens = new RouteValueDictionary()
            };
            
            routes.Add(name, route);

            return route;
        }
    }
}