using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface ICourseSetsManager
    {
        bool SaveExamSet(string courseid, QuestionSet examquestionSet);
        bool SaveExamSet(string courseid, IList<QuestionSet> examquestionSets);
        bool SavePracticeSet(string courseid, QuestionSet practicequestionset);
        bool SavePracticeSets(string courseid, IList<QuestionSet> practicequestionsets);
        bool SaveSubjectPracticeSet(string courseid, string subjectid, QuestionSet practicequestionset);
        bool SaveSubjectPracticeSets(string courseid, string subjectid, IList<QuestionSet> practicequestionsets);
        IList<QuestionSet> GetAllExamSet(string courseid);
        IList<QuestionSet> GetAllPracticeSet(string courseid);
        IList<QuestionSet> GetAllSubjectPracticeSet(string courseid, string subjectid);
    }
}
