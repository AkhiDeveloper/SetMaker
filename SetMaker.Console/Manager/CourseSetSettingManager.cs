using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class CourseSetSettingManager
        : ICourseSetSettingManager
    {
        private string _directory;
        private ICourseManager _courseManager;

        public CourseSetSettingManager(CourseManager courseManager)
        {
            _directory = @"D:\Downloads\CourseSetsettings";
            _courseManager =courseManager;
        }
        public bool CreateSetSetting(string courseid, CourseSetSetting setting)
        {
            
        }

        public CourseSetSetting? GetCourseSetSetting(string courseid)
        {
            throw new NotImplementedException();
        }
    }
}
