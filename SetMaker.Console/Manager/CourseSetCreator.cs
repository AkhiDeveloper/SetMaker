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
        private ICourseSetManager _courseSetManager;

        public CourseSetCreator(ICourseManager courseManager)
        {
            _courseManager = courseManager;
            _courseSetManager = new CourseSetManager();
        }

        private IList<int> _RandomQuestionsSN(int totalquestions, int? requriedquestions = null)
        {
            IList<int> result = new List<int>();
            if (totalquestions < requriedquestions || requriedquestions == null)
            {
                requriedquestions = totalquestions;
            }
            Random random = new Random();
            var randomsequence = Enumerable.Range(1, totalquestions)
                .Select(i => new Tuple<int, int>(random.Next(totalquestions), i))
                .OrderBy(i=>i.Item1)
                .Select(i=>i.Item2)
                .ToList();
            for(int i= 0; i < requriedquestions; i++)
            {
                result.Add(randomsequence[i]);
            }
            return result;
        }

        private Set _CreateCourseSet(Course course, CourseSetSetting setting)
        {
            Set courseSet = new Set()
            {
                id = course.id +"@"+ DateTime.Now.Ticks.ToString(),
                setNumber=_courseSetManager.GetAllExamSet(course.id)==null?1: _courseSetManager.GetAllExamSet(course.id).Count()+1,
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
            if (subjectquestions.Count() < question_in_sets) question_in_sets = subjectquestions.Count();
            int a, b, c, d;
            a = Math.DivRem(subjectquestions.Count(), question_in_sets, out b);
            c = Math.DivRem(b, a, out d);
            int no_of_sets = a;
            int question_in_first_set = question_in_sets + c + d;
            int question_in_other_set = question_in_sets + c;
            int question_in_set = question_in_first_set;
            int count = 0;
            Set set = new Set()
            {
                topicName = _courseManager.GetSubjectIdandNamePairs(course.id).Single(x => x.Key == subjectcode).Value,
                id = course.id + "@" + subjectcode + "@" + DateTime.Now.Ticks.ToString(),
                setNumber = sets.Count + 1,
                createdDate = DateTime.Now,
                completionTime = null,
                negativeMarking = null
            };
            foreach (var sn in questionSn)
            {
                if (!(count < question_in_set))
                {
                    sets.Add(set);
                    question_in_set = question_in_other_set;
                    set = new Set()
                    {
                        topicName = _courseManager.GetSubjectIdandNamePairs(course.id).Single(x => x.Key == subjectcode).Value,
                        id = course.id + "@" + subjectcode + "@" + DateTime.Now.Ticks.ToString(),
                        setNumber = sets.Count + 1,
                        createdDate = DateTime.Now,
                        completionTime = null,
                        negativeMarking = null
                    };
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
            try
            {
                if (_courseManager.GetSubjectsId(course.id).Contains(subjectcode))
                {
                    try
                    {
                        
                        foreach (var set in _CreateSubjectSets(course, subjectcode, question_in_each_set))
                        {
                            try
                            {
                                result.Add(set);
                            }
                            catch(Exception ex)
                            {
                                continue;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            catch
            {
                //do nothing
            }
            
            return result;
        }
    }
}
