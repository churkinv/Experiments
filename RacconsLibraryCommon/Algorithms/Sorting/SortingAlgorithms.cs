using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon
{
    /// <summary>
    /// This class represents set of methods which implements main SORTING algorighms.
    /// Этот класс содержит набор методов реализовывающие основные алгоритмы СОРТИРОВКИ.
    /// </summary>
    public static class SortingAlgorithms
    {
        /// <summary>
        /// Worst and average: O(n^2).
        /// Best: O(n), up to 10 digits.
        /// Operates directly on the array no additional memory allocation.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Tuple<int[], int, int> BubleSort(int[] array)
        {
            int swapsCount = 0;
            int iterationCount = 0;
            int i = 0;
            while (i < array.Length - 1)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        swapsCount++;
                    }
                    iterationCount++;
                }
                iterationCount++;
                i++;
            }
            return Tuple.Create(array, swapsCount, iterationCount);
        }

        /// <summary>
        ///  Worst and average: O(n^2),  Θ(n^2) worst case running.
        ///  Best O(n), up to 10 digits.
        ///  Operates directly on the array no additional memory allocation.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] InsertionSort(int[] array)
        {
            int element = array[0];
            int j = 0;

            for (int i = 1; i < array.Length; i++)
            {
                if (element > array[i])
                {
                    int ins = array[i];
                    array[i] = element;
                    j = 0;
                    while (j <= i)
                    {
                        if (array[j] > ins)
                            Swap(ref array[j], ref ins);
                        j++;
                    }
                }
                else
                {
                    element = array[i];
                }
            }
            return array;
        }

        /// <summary>
        /// Worst and average O(n^2) than BubbleSort, but worse than insertion sort.
        /// Best perfomance 0(n^2) - as anywayyou have go through all array to find the min etc.
        /// Space required O(n)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] SelectionSort(int[] array)
        {
            for (int j = 0; j < array.Length; j++)
            {
                for (int i = j+1; i < array.Length; i++)
                {
                    if (array[i] < array[j])
                    {
                        Swap(ref array[i], ref array[j]);
                    }
                }
            }
            return array;
        }

        /// <summary>
        /// Worst, average, best case O(n Log n).
        /// Space required O(n), but usually it needs extra allocation of memory.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void MergeSort(int[] a)
        {
            if (a.Length <= 1) return;

            int leftSize = a.Length / 2;
            int rightSize = a.Length - leftSize;

            int[] left = new int[leftSize];
            int[] right = new int[rightSize];

            Array.Copy(a, 0, left, 0, leftSize);
            Array.Copy(a, leftSize, right, 0, rightSize);

            MergeSort(left);
            MergeSort(right);

            MergeArray(a, left, right);
        }

        /// <summary>
        /// Worst case O(n^2) - not appropriate for large patalogically sorted (inverse sorted) data sets.
        /// Average  O(n Log n), appropriate for large data sets.
        /// Best case O(n Log n), very good at small and nearly sorted data sets.
        /// Space required: O(n), array space and stack space must be considered, there exist optimizations to
        /// reduce space usage furhter.
        /// </summary>
        public static void QuickSort(int[] array)
        {
            //          if (left<right)

            //QuickSort ()


            int j = array[array.Length - 1];
            int pivot = array[j];
            int wall = -1;
            for (int i = 0; i < array.Length - 2; i++)
            {
                int left = array[i];
                if (left < pivot)
                {
                    wall++;
                    Swap(ref array[i], ref array[wall]);
                }
            }
            Swap(ref pivot, ref array[wall + 1]);
            int newPivot = array[array.Length - 1];


        }

        #region Helper methods for sorting: swap, merge, partition

        public static void Partition(int[] array, int wall, int pivot)
        { }

        public static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        public static void MergeArray(int[] target, int[] left, int[] right)
        {
            int rightIndex = 0;
            int leftIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    target[targetIndex] = right[rightIndex];
                    rightIndex++;

                }
                else if (rightIndex >= right.Length)
                {
                    target[targetIndex] = left[leftIndex];
                    leftIndex++;

                }
                else if (left[leftIndex] < right[rightIndex])
                {
                    target[targetIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    target[targetIndex] = right[rightIndex];
                    rightIndex++;
                }
                remaining--;
                targetIndex++;
            }
        }
        #endregion
    }
}
