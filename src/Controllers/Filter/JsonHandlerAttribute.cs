using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateMVC5_JSONRpc.Results;

namespace TemplateMVC5_JSONRpc.Filters
{
    /// <summary>
    /// Filtro que intercepta resultados do tipo JsonResult, substituindo-o por um outro tipo de resultado
    /// que usa JSON.NET para serializar os dados originais
    /// </summary>
    public class JsonHandlerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //É um JSONResult mas não um JSONNetResult
            var isOnlyJsonResult = filterContext.Result is JsonResult && !(filterContext.Result is JsonNetResult);

            if (isOnlyJsonResult )
            {
                var jsonResult = filterContext.Result as JsonResult;

                filterContext.Result = new JsonNetResult
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior
                };
            }

            base.OnActionExecuted(filterContext);
        }
    }
}