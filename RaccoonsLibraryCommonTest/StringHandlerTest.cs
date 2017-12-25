using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaccoonsLibraryCommon;

namespace RaccoonsLibraryCommonTest
{
    [TestClass]
    public class StringHandlerTest
    {
        [TestMethod]
        public void InsertSpaceTestValid()
        {
            //Arrange
            var source = "VitaliyMelnikVsevolodivich";
            var expected = "Vitaliy Melnik Vsevolodivich";

            //Act
            var actual = source.InsertSpaces();

            //Assert
            Assert.AreEqual(expected, actual);
            //Assert.AreNotEqual(expected, actual); 
        }

        [TestMethod]
        public void InsertSpaceTestWithExistingSpaceTest()
        {
            //Arrange
            var source = "Sonic Screwdriver";
            var expected = "Sonic Screwdriver";
                        
            //Act

            var actual = source.InsertSpaces();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertStringToByteAndBackTest()
        {
            //Arrange
            var salt = "Fighter";
            var source = "Sonic Screwdriver";
            var expected = "Sonic Screwdriver";

            //Act
    
            var buffer = source.GetBytes();
            var actual = buffer.GetString();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertStringToByteAndBackEncodedTest()
        {
            //Arrange
            var salt = "Fighter";
            var source = "Sonic Screwdriver";
            var expected = "Sonic Screwdriver";

            //Act

            var buffer = source.GetBytesEncoded("UNICODE");
            var actual = buffer.GetStringEncoded("UNICODE");

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
