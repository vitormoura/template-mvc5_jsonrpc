using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TemplateMVC5_JSONRpc.Filters;
using TemplateMVC5_JSONRpc.JSONRPC;

namespace TemplateMVC5_JSONRpc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            ModelBinderProviders.BinderProviders.Add(new JsonRPCModelBinderProvider());

            //Filtros
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalFilters.Filters.Add(new JsonHandlerAttribute());

            //Chamadas JSON-RPC
            RouteTable.Routes.MapJsonRpcRoute("json-rpc", "rpc/");

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
