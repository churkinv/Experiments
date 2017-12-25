using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon
{
    /// <summary>
    /// This class represents set of methods which implements main SEARCHING algorighms.
    /// Этот класс содержит набор методов реализовывающие основные алгоритмы ПОИСКА.
    /// </summary>
    public static class SearchingAlgorithms
    {
        /// <summary>
        /// O(Log n)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int? IntBinarySearch(int[] array, int a)
        {
            if (array.Length == 0) return null;
            if (a < array[0] || a > array[array.Length - 1]) return null;

            int first = 0;
            int last = array.Length - 1;
            
            while (first<=last)
            {
                int mid = first + ((last-first) / 2);
                if (a == array[mid])
                {
                    return mid;
                }
                else if (a < array[mid])
                {
                    last = mid-1;
                }
                else if (a > array[mid])
                {
                    first = mid+1;
                }
                else
                    return null;
            }
            return null;
        }

        public static void StringBinarySearch() {

        }

        //Array list
        //Linked list
        //Hash Set/Hash Map
        //Tree Set/Tree Map(Red-Black tree)
        //Binary Search
        //Merge Sort
        //Quick Sort


    }
}
