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


        [Fact]
        public void GetOpeningTag_TagNameImg()
        {
            //Arrange
            string text = "<img>https://drive.google.com/file/d/1-eow7pMimtO__eU5faJTG9fIDEkb7EUK/view?usp=share_link</img>";
            string expected = "img";
                
            //Act
            string actual = text.GetOpeningTag();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTagText_TagNameImg()
        {
            //Arrange
            string tagName = "img";
            string text = "<img>https://drive.google.com/file/d/1-eow7pMimtO__eU5faJTG9fIDEkb7EUK/view?usp=share_link</img>";
            string expected = "https://drive.google.com/file/d/1-eow7pMimtO__eU5faJTG9fIDEkb7EUK/view?usp=share_link";

            //Act
            string actual = text.GetTagText(tagName);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}