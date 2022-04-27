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
    public class CourseManagerTest
    {
        [Fact]
        public void SaveCourseTest_iscoursesaved()
        {
            //Arrange
            Course course = new Course() { id = "testcourse01", name = "testcourse" };
            course.books.Add(new Book() { id = "Book01", name = "Book" });
            string expectedcoursecode = course.id;

            //Act
            ICourseManager courseManager = new CourseManager();

            //Arrange
            Assert.True(courseManager.SaveCourse(course));
        }

        [Fact]
        public void GetCourse_checkidandname()
        {
            //Arrange
            Course expectedcourse = new Course() { id = "testcourse01", name = "testcourse" };
            expectedcourse.books.Add(new Book() { id = "Book01", name = "Book" });

            //Act
            ICourseManager courseManager = new CourseManager();
            Course actualcourse = courseManager.GetCourse(expectedcourse.id);
            //Arrange
            Assert.Equal(expectedcourse.id, actualcourse.id);
            Assert.Equal(expectedcourse.name, actualcourse.name);
            Assert.Equal(expectedcourse.books.First().name, actualcourse.books.First().name);
        }
    }
}
