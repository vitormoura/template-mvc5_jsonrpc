using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemplateMVC5_JSONRpc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Hello(String mensagem)
        {
            return Content("Sua mensagem: " + mensagem);
        }
    }
}