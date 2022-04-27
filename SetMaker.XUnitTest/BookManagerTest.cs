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
            Book book = new Book() { id = "testbook01", name = "testbook"  };
            book.subjects.Add(new Subject() { id = "Subject01", name = "Subject" });
            string expectedbookcode = book.id;

            //Act
            IBookManager bookManager = new BookManager();

            //Arrange
            Assert.True(bookManager.SaveBook(book));
        }

        [Fact]
        public void GetBook_checkidandname()
        {
            //Arrange
            Book expectedbook = new Book() { id = "testbook01", name = "testbook" };
            expectedbook.subjects.Add(new Subject() { id = "Subject01", name = "Subject" });

            //Act
            IBookManager bookManager = new BookManager();
            Book actualbook = bookManager.GetBook(expectedbook.id);
            //Arrange
            Assert.Equal(expectedbook.id, actualbook.id);
            Assert.Equal(expectedbook.name, actualbook.name);
            Assert.Equal(expectedbook.subjects.First().name, actualbook.subjects.First().name);
        }
    }
}
