using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaccoonsLibraryCommon;
using RacconsLibraryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon.Test
{
    [TestClass()]
    public class SortingAlgorithmsTest
    {
        [TestMethod()]
        public void BubleSortTest()
        {
            //Arrange
            int[] unsorted = new int[] { 2, 3, 4, 8, 9, 1 };
            int[] expected = new int[] { 1, 2, 3, 4, 8, 9 };

            //Act
            int[] actual = SortingAlgorithms.BubleSort(unsorted).Item1;

            //Assert 
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void InsertionSortTest()
        {
            //Arrange
            //int[] unsorted = new int[] { 2, 5, 4, 0, 9, 1 };
            int[] unsorted = new int[] { 9, 1, 1, 5, 4, 0 };
            int[] expected = new int[] { 0, 1, 1, 4, 5, 9 };

            //Act
            int[] actual = SortingAlgorithms.InsertionSort(unsorted);

            //Assert 
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SwapTest()
        {
            //Arrange
            int a = 10;
            int b = 5;

            int expectedA = 5;
            int expectedB = 10;
            string expected = "510";

            //Act
            SortingAlgorithms.Swap(ref a, ref b);
            string actual = a.ToString() + b.ToString();

            //Assert
            //Assert.AreEqual(expectedA,a);
            Assert.AreEqual(expected, actual);


        }

        [TestMethod()]
        public void MergeSortTest()
        {
            //Arrange
            //int[] unsorted = new int[] { 2, 5, 4, 0, 9, 1 };
            int[] unsorted = new int[] { 9, 1, 5, 2, 4, 0 };
            int[] expected = new int[] { 0, 1, 2, 4, 5, 9 };

            //Act
            SortingAlgorithms.MergeSort(unsorted);

            //Assert 
            CollectionAssert.AreEqual(expected, unsorted);
        }

        [TestMethod()]
        public void QuickSortTest()
        {
            //Arrange
            int[] unsorted = new int[] { 9, 1, 5, 2, 4, 0 };
            int[] expected = new int[] { 0, 1, 2, 4, 5, 9 };

            //Act
            SortingAlgorithms.QuickSort(unsorted);

            //Assert 
            CollectionAssert.AreEqual(expected, unsorted);
        }

        [TestMethod()]
        public void SelectionSortTest()
        {
            //Arrange
            int[] unsorted = new int[] { 9, 1, 5, 2, 4, 0 };
            int[] expected = new int[] { 0, 1, 2, 4, 5, 9 };

            //Act
            SortingAlgorithms.SelectionSort(unsorted);

            //Assert 
            CollectionAssert.AreEqual(expected, unsorted);
        }
    }
}
