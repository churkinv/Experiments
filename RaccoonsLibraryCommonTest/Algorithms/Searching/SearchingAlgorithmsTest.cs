using Microsoft.VisualStudio.TestTools.UnitTesting;
using RacconsLibraryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacconsLibraryCommon.Test
{
    [TestClass()]
    public class SearchingAlgorithmsTest
    {
        [TestMethod()]
        public void IntBinarySearchTest()
        {
            //Arrange            
            int[] array = new int[] { 0, 1, 1, 4, 5, 9 };
            int? expected = 0;

            //Act
            int? actual = SearchingAlgorithms.IntBinarySearch(array, 0);

            //Assert 
            Assert.AreEqual(expected, actual);
        }
    }

}