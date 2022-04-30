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
        bool SaveExamSet(Course course, Set examquestionSet);
        bool SaveExamSets(Course course, IList<Set> examquestionSets);
        bool SavePracticeSet(Course course, Set practicequestionset);
        bool SavePracticeSets(Course course, IList<Set> practicequestionsets);
        bool SaveSubjectPracticeSet(Course course, string subjectid, Set practicequestionset);
        bool SaveSubjectPracticeSets(Course course, string subjectid, IList<Set> practicequestionsets);
        IList<Set>? GetAllExamSet(string courseid);
        IList<Set>? GetAllPracticeSet(string courseid);
        IList<Set>? GetAllSubjectPracticeSet(string courseid, string subjectid);
    }
}
