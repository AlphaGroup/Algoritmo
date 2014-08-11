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
            var bubble = new BubbleSort<int>();
            char[] delimiter = { ' ' };
            string[] valuesStr = input.Split(delimiter);
            var values = new List<int>();
            foreach (var str in valuesStr)
            {
                values.Add(int.Parse(str));
            }
            bubble.Sort(values);
            return Json(bubble.GetListForJson(), JsonRequestBehavior.AllowGet);
        }
    }
}