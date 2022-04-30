using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class CourseSetCreator
        : ICourseSetsCreator
    {
        private ICourseManager _courseManager;

        public CourseSetCreator(CourseManager courseManager)
        {
            _courseManager = courseManager;
        }

        private IList<int> _RandomQuestionsSN(int totalquestions, int? requriedquestions = null)
        {
            IList<int> result = new List<int>();
            if (totalquestions < requriedquestions || requriedquestions == null)
            {
                requriedquestions = totalquestions;
            }
            for (int i = 0; i < requriedquestions; i++)
            {
                Random random = new Random();
                var questionnumber = random.Next(1, totalquestions);
                if (result.Any(x => x == questionnumber))
                {
                    i--;
                    continue;
                }
                result.Add(questionnumber);
            }
            return result;
        }

        private Set _CreateCourseSet(Course course, CourseSetSetting setting)
        {
            Set courseSet = new Set()
            {
                id = course.id + DateTime.Now.Ticks.ToString(),
                topicName = course.name,
                createdDate=DateTime.Now,
                completionTime=setting.completionTime,
                negativeMarking=setting.negativemarking,
            };
            foreach (var item in setting.subjectquestions)
            {
                var subjectcode = item.Key;
                if (!_courseManager.HasSubject(course, subjectcode)) continue;
                var subjectquestions = _courseManager.GetQuestions(course.id, subjectcode);
                if(subjectquestions == null)
                    return courseSet;
                int totalquestions = subjectquestions.Count();
                IList<int> filteredQuestionsSn = _RandomQuestionsSN(totalquestions, item.Value);
                foreach(var questionSn in filteredQuestionsSn)
                {
                   courseSet.questions.Add(subjectquestions[questionSn - 1]);
                }
            }
            return courseSet;
        }

        private ICollection<Set>? _CreateSubjectSets(Course course, string subjectcode, int question_in_sets)
        {
            var subjectquestions = _courseManager.HasSubject(course, subjectcode) ?
                _courseManager.GetQuestions(course.id, subjectcode) : null;
            if (subjectquestions == null)
                return null;
            IList<Set> sets = new List<Set>();
            var questionSn = _RandomQuestionsSN(subjectquestions.Count());
            if (subjectquestions.Count() > question_in_sets) question_in_sets = subjectquestions.Count();
            int a, b, c, d;
            a = Math.DivRem(subjectquestions.Count(), question_in_sets, out b);
            c = Math.DivRem(b, a, out d);
            int no_of_sets = a;
            int question_in_first_set = question_in_sets + c;
            int question_in_other_set = question_in_sets + c + d;
            int question_in_set = question_in_first_set;
            int count = 0;
            Set set = new Set();
            foreach (var sn in questionSn)
            {
                if (!(count < question_in_set))
                {
                    sets.Add(set);
                    question_in_set = question_in_other_set;
                    set = new Set();
                    count = 0;
                }
                set.questions.Add(subjectquestions[sn - 1]);
                count++;
            }
            sets.Add(set);
            return sets;
        }

        public Set? CreateExamSet(Course course, CourseSetSetting setSetting)
        {
            if (setSetting == null)
            {
                return null;
            }
            Set result=new Set();
            result=_CreateCourseSet(course, setSetting);
            if (result == null)
            {
                return null;
            }
            result.IsTimeRestrictionApplied = true;
            return result;
        }

        public IList<Set> CreatePracticeSets(Course course, int question_in_each_set)
        {
            throw new NotImplementedException();
        }

        public IList<Set>? CreateSubjectPracticeSets(Course course, string subjectcode, int question_in_each_set)
        {
            IList<Set> result = new List<Set>();
            if (_courseManager.GetSubjectsId(course.id) == null)
                return null;
            foreach (var id in _courseManager.GetSubjectsId(course.id))
            {
                if(_CreateSubjectSets(course, subjectcode, question_in_each_set)==null)
                    return null;
                foreach (var set in _CreateSubjectSets(course, subjectcode, question_in_each_set))
                {
                    result.Add(set);
                }
            }
            return result;
        }
    }
}
