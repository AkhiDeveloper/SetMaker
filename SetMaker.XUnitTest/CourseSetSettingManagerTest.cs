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
    public class CourseSetSettingManagerTest
    {
        [Fact]
        public void SaveSetSettingTest_issaved()
        {
            //Arrange
            string courseid = "testcourse01";
            CourseManager courseManager = new CourseManager();
            CourseSetSettingManager manager = new CourseSetSettingManager(courseManager);            
            var course = courseManager.GetCourse(courseid);
            IDictionary<string, int> questions= new Dictionary<string, int>();
            foreach(var subject in courseManager.GetSubjectsId(course.id))
            {
                questions.Add(subject, 10);
            }
            CourseSetSetting courseSetSetting = new CourseSetSetting()
            {
                subjectquestions = questions,
                completionTime = TimeSpan.FromMinutes(30),
                negativemarking = 0.1M,
            };

            //Act
            var actual=manager.SaveSetSetting(courseid, courseSetSetting);

            //Arrest
            Assert.True(actual);
        }
    }
}
