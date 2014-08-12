using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Animation()
        {
            return View();
        }
        public ActionResult Algorithm()
        {
            return View();
        }
    }
}