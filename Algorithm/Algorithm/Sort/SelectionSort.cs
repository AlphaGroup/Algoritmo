/*
 * O(n^2)
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class SelectionSort<T> : ISort<T>, IActionProvider
    {
        private readonly List<object> _actionList = new List<object>();
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            // For JSON
            _actionList.Clear();
            // End for JSON
            int len = inList.Count;
            for (int i = 0; i < len - 1; ++i)
            {
                // Find the smallest one among the [i...n]
                int smallest = i;
                // For JSON
                _actionList.Add(new { action = "MARK", param = string.Format(@"{0}", i) });
                var key = inList[i];
                for (int j = i + 1; j < len; ++j)
                {
                    if (comparer.Compare(key, inList[j]) > 0)
                    {
                        key = inList[j];
                        // For JSON
                        _actionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(smallest + 1)) });
                        smallest = j;
                        // For JSON
                        _actionList.Add(new { action = "MARK", param = string.Format(@"{0}", smallest) });
                    }
                }
                // For JSON
                _actionList.Add(new { action = "MARK", param = string.Format(@"{0}", -(smallest + 1)) });
                if (smallest != i)
                {
                    Exchange(inList, smallest, i);
                    // For JSON
                    _actionList.Add(new { action = "EXCG", param = string.Format(@"{0},{1}", smallest, i) });
                }
            }
        }

        private void Exchange(List<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public List<object> GetListForJson()
        {
            return _actionList;
        }
    }
}
