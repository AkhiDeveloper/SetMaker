using Xunit;
using SetMaker.Console.Reader;
using System.Collections.Generic;
using SetMaker.Console.Models;
using System.Linq;

namespace SetMaker.XUnitTest
{

    public class QuestionReaderTest
    {
        #region ReadCorrectOptionsfromfileTest
        [Fact]
        public void ReadCorrectOptionsfromfileTest_checknumberofanswerinfile()
        {
            //Arrange
            string filepath = @"D:\Projects\QuestionOptionExtractor\UnitTest\test.csv";
            int expectedoptionspair = 7;

            //Act
            IQuestionReader reader = new QuestionReader();
            int actualpair=reader.ReadCorrectOptionsfromfile(filepath).Count;

            //Assert
            Assert.Equal(expectedoptionspair, actualpair);
        }

        [Fact]
        public void ReadCorrectOptionsfromfileTest_CheckvalidPair()
        {
            //Arrange
            string filepath = @"D:\Projects\QuestionOptionExtractor\UnitTest\test.csv";
            int key = 122;
            string symbol = "d";
            //Act
            IQuestionReader reader = new QuestionReader();
            var optionpairs=reader.ReadCorrectOptionsfromfile(filepath);
            string actualsymbol;


            //Assert
            Assert.True(optionpairs.TryGetValue(key,out actualsymbol));
            Assert.Equal(symbol, actualsymbol);
        }
        #endregion

        #region ReadQuestionsfromfile
        [Fact]
        public void ReadQuestionsfromfileTest_NumberofQuestion()
        {
            //Arrange
            string filepath = @"D:\Projects\QuestionOptionExtractor\UnitTest\mock_test_questions.txt";
            ICollection<Question> expected_result = new List<Question>()
            {
                new Question(1,"The deformation per unit length is called"),
                new Question(2,"The property of material to deform permanently without breaking and having high strength is known as"),
                new Question(3,"The ability of material to deform permanently under compression without breaking"),
            };
            IQuestionReader questionReader = new QuestionReader();
            IQueryable<Question>? outputquestion;
            //Act
            outputquestion = questionReader.ReadQuestionsfromfile(filepath).AsQueryable();
            //Assert
            Assert.True(outputquestion.Count() == 2);
            Assert.Equal(4, outputquestion.First().options.Count());
        }
        #endregion

    }
}
