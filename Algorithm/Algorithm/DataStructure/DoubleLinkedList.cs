/*
 * P237
 * Implement of the double linked list.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.DataStructure
{
    class DoubleLinkedList<T>
    {
        // Nested class are private by default
        public class DoubleLinkedListObject<TP>
        {
            public TP Key { get; set; }
            public DoubleLinkedListObject<TP> Prev { get; set; }
            public DoubleLinkedListObject<TP> Next { get; set; }
        }

        // Start the code about LinkList itself
        private DoubleLinkedListObject<T> _head = null;

        // For debug
        public T this[int index]
        {
            get
            {
                DoubleLinkedListObject<T> obj = this._head;
                for (int i = 0; i < index; ++i)
                {
                    obj = obj.Next;
                }
                if (obj == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    return obj.Key;
                }
            }
        }

        // The insert operation inserts the new element onto the front of the list.
        // Won't clone the input key.
        // Time: O(1)
        public void Insert(T key)
        {
            var newObj = new DoubleLinkedListObject<T>();
            newObj.Key = key;
            newObj.Next = this._head;
            // There are some elements before insertion.
            if (this._head != null)
            {
                this._head.Prev = newObj;
            }
            this._head = newObj;
            newObj.Prev = null;
        }

        // Search the element with specified key.
        // Time: theta(n)
        public DoubleLinkedListObject<T> Search(T key)
        {
            return Search(key, Comparer<T>.Default);
        }
        public DoubleLinkedListObject<T> Search(T key, IComparer<T> comparer)
        {
            var elem = this._head;
            // Skip the elements with different keys.
            while (elem != null && comparer.Compare(elem.Key, key) != 0)
            {
                elem = elem.Next;
            }
            return elem;
        }

        // Delete the element by the elements reference, not by the key.
        // We usually use search operation first to find the reference.
        // Time: O(1) without search operation.
        public void Delete(DoubleLinkedListObject<T> obj)
        {
            // Deal with the previous one of obj.
            if (obj.Prev != null)
            {
                // Obj is an element among others.
                obj.Prev.Next = obj.Next;
            }
            else
            {
                // Obj is the first element.
                this._head = obj.Next;
            }
            // Deal with the next one of obj.
            if (obj.Next != null)
            {
                // Obj is not the last one.
                obj.Next.Prev = obj.Prev;
            }
        }
    }
}
