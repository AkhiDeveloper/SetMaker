using SetMaker.Console.Manager;
using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SetMaker.XUnitTest
{
    public class BookManagerTest
    {
        [Fact]
        public void SaveBookTest_isbooksaved()
        {
            //Arrange
            Book book = new Book() { id = "testbook01", name = "testbook" };
            string expectedbookcode = book.id;

            //Act
            IBookManager bookManager = new BookManager();

            //Arrange
            Assert.True(bookManager.SaveBook(book));
        }
    }
}
