using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface ICourseSetManager
    {
        bool SaveExamSet(Course course, QuestionSet examquestionSet);
        bool SaveExamSets(Course course, IList<QuestionSet> examquestionSets);
        bool SavePracticeSet(Course course, QuestionSet practicequestionset);
        bool SavePracticeSets(Course course, IList<QuestionSet> practicequestionsets);
        bool SaveSubjectPracticeSet(Course course, string subjectid, QuestionSet practicequestionset);
        bool SaveSubjectPracticeSets(Course course, string subjectid, IList<QuestionSet> practicequestionsets);
        IList<QuestionSet>? GetAllExamSet(string courseid);
        IList<QuestionSet>? GetAllPracticeSet(string courseid);
        IList<QuestionSet>? GetAllSubjectPracticeSet(string courseid, string subjectid);
    }
}
