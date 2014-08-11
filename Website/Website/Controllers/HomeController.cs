using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Algorithm.Sort;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Deal with the input 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult RequireActionsAjax(string type, string input)
        {
            var actions = new List<object>();
            actions.Add(new { action = "EXCG", param = "0,1" });
            actions.Add(new { action = "EXCG", param = "1,2" });
            actions.Add(new { action = "EXCG", param = "2,3" });
            return Json(actions, JsonRequestBehavior.AllowGet);
        }
    }
}