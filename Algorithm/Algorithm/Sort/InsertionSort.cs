using System.Collections.Generic;
using System.Linq;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    internal class InsertSort<T> : ISort<T>, IActionProvider
    {
        private readonly List<object> _actionList = new List<object>();

        public List<object> GetListForJson()
        {
            return _actionList;
        }

        /// <summary>
        ///     Use default comparer.
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        /// <summary>
        ///     Use inputed comparer.
        /// </summary>
        /// <param name="inList"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            // Clear action list
            _actionList.Clear();
            // Start sorting
            for (int j = 1; j < inList.Count; ++j)
            {
                // Choose a key
                T key = inList.ElementAt(j);
                // JSON
                _actionList.Add(new {action = "ASGN", param = string.Format(@"{0}={1}", -1, j)});
                int i = j - 1;
                while (i >= 0 && comparer.Compare(inList[i], key) > 0)
                {
                    inList[i + 1] = inList[i];
                    // JSON
                    _actionList.Add(new {action = "ASGN", param = string.Format(@"{0}={1}", i + 1, i)});
                    --i;
                }
                inList[i + 1] = key;
                // JSON
                _actionList.Add(new {action = "ASGN", param = string.Format(@"{0}={1}", i + 1, -1)});
            }
        }
    }
}