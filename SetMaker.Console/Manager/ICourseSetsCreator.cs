using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface ICourseSetsCreator
    {
        Set CreateExamSet(Course course,CourseSetSetting setSetting);
        IList<Set> CreatePracticeSets(Course course, int question_in_each_set);
        IList<Set> CreateSubjectPracticeSets(Course course, string subjectcode, int question_in_each_set);
    }
}
