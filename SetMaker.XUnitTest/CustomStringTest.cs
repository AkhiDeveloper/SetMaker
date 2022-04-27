using Xunit;
using SetMaker.Console;

namespace SetMaker.XUnitTest
{
    public class CustomStringTest
    {
        [Fact]
        public void TrimSymbolStartTest_trimalltypeofsymbol_textstartwithnumber()
        {
            //arrange
            string text = "!@#$%^&*()_+-=[]; \"\\<>,./: : 1) trim @the !text#-=-5\\][;'.,";
            string expected = "1) trim @the !text#-=-5\\][;'.,";

            //act
            string result=CustomString.TrimSymbolStart(text);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TrySeperateStartNumberAndLettersStart_detecttextstartwithnumber()
        {
            //Arrange 
            string text = "11 2*+1) ajbegfegf747##@544 545";
            int expectednumber = 11;
            string expectedstring = " 2*+1) ajbegfegf747##@544 545";
            bool expected=true;

            //Act
            int? resultnumber;
            string resultstring;
            bool result = CustomString.TrySeperateStartNumberAndLetters(text, out resultnumber, out resultstring);

            //Assert
            Assert.Equal(expected, result);
            Assert.Equal(expectednumber, resultnumber);
            Assert.Equal(expectedstring, resultstring);
        }

        [Fact]
        public void TrySeperateStartNumberAndLettersStart_detecttextnotstartwithnumber()
        {
            //Arrange 
            string text = " 11 2*+1) ajbegfegf747##@544 545";
            int? expectednumber = null;
            string expectedstring = text;
            bool expected = false;

            //Act
            int? resultnumber;
            string resultstring;
            bool result = CustomString.TrySeperateStartNumberAndLetters(text, out resultnumber, out resultstring);

            //Assert
            Assert.Equal(expected, result);
            Assert.Equal(expectednumber, resultnumber);
            Assert.Equal(expectedstring, resultstring);
        }
    }
}