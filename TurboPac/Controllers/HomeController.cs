using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurboPac.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de esta plicatoón";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Informacion de COntacto";

            return View();
        }
    }
}