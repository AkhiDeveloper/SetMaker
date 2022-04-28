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
        QuestionSet CreateExamSet(CourseSetSetting setSetting);
        IList<QuestionSet> CreatePracticeSets(int question_in_each_set);
        IList<QuestionSet> CreateSubjectPracticeSets(string subjectcode, int question_in_each_set);
    }
}
