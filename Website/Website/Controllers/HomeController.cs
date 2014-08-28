using System.Collections.Generic;
using System.Web.Mvc;
using Algorithm.Interface;
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
        ///     Deal with the input
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult RequireActionsAjax(string queryType, string algorithm, string input)
        {
            // Get input values
            char[] delimiter = { ' ' };
            string[] valuesStr = input.Split(delimiter);
            var values = new List<int>();
            foreach (string str in valuesStr)
            {
                values.Add(int.Parse(str));
            }

            if (queryType == "SORT")
            {
                IActionProvider provider;
                ISort<int> sorter;
                switch (algorithm)
                {
                    case "BUBBLE":
                        var bubble = new BubbleSort<int>();
                        sorter = bubble;
                        provider = bubble;
                        break;
                    case "INSERTION":
                        var insertion = new InsertSort<int>();
                        sorter = insertion;
                        provider = insertion;
                        break;
                    case "MERGE":
                        var merge = new MergeSort<int>();
                        sorter = merge;
                        provider = merge;
                        break;
                    case "QUICK":
                        var quick = new QuickSort<int>();
                        sorter = quick;
                        provider = quick;
                        break;
                    case "SELECTION":
                        var selection = new SelectionSort<int>();
                        sorter = selection;
                        provider = selection;
                        break;
                    default:
                        // Default case will use bubble sort
                        var defaultSort = new BubbleSort<int>();
                        sorter = defaultSort;
                        provider = defaultSort;
                        break;
                }
                sorter.Sort(values);
                return Json(provider.GetListForJson(), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}