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
        Course? GetCourse(string id);
        IList<string>? GetSubjectsId(string courseid);
        IDictionary<string, string> GetSubjectIdandNamePairs(string courseid);
        IList<Question>? GetQuestions(string courseid);
        IList<Question>? GetQuestions(string courseid, string subjectid);
    }
}
