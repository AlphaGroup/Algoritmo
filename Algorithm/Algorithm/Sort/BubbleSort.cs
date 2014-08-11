using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Interface;

namespace Algorithm.Sort
{
    class BubbleSort<T> : ISort<T>, IActionProvider
    {
        private List<object> _actionListlist = new List<object>();
        public void Sort(List<T> inList)
        {
            Sort(inList, Comparer<T>.Default);
        }

        public void Sort(List<T> inList, IComparer<T> comparer)
        {
            // Clear action list
            _actionListlist.Clear();
            // Start sorting
            int length = inList.Count;
            for (int i = 0; i < length - 1; ++i)
            {
                for (int j = length - 1; j > i; --j)
                {
                    if (comparer.Compare(inList[j - 1], inList[j]) > 0)
                    {
                        // Exchange j-1 and j
                        T temp = inList[j];
                        inList[j] = inList[j - 1];
                        inList[j - 1] = temp;
                        _actionListlist.Add(new { action = "EXCG", param = string.Format("{0},{1}", j - 1, j) });
                    }
                }
            }
        }

        public List<object> GetListForJson()
        {
            return _actionListlist;
        }
    }
}
