using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Interface
{
    interface IActionProvider
    {
        /// <summary>
        /// Returns a list of objects which is used to be transformed into JSON format.
        /// </summary>
        /// <returns></returns>
        List<object> GetListForJson();
    }
}
