using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface ICourseManager
    {
        bool SaveCourse(Course course);
        Course GetCourse(string id);
    }
}
