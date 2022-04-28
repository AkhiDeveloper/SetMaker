using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface ICourseSetSettingManager
    {
        bool CreateSetSetting(string courseid, CourseSetSetting setting);
        CourseSetSetting? GetCourseSetSetting(string courseid); 
    }
}
