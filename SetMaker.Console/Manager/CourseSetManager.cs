using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class CourseSetManager
        : ICourseSetManager
    {
        private string _directory;
        private ICourseManager _courseManager;
        private string _examsetfoldername;
        private string _practicesetfoldername;
        private string _subjectpracticesetfoldername;
   
        public CourseSetManager()
        {
            _directory = @"D:\Downloads\CourseSets";
            _examsetfoldername = "Exam Set";
            _practicesetfoldername = "Practice Set";
            _subjectpracticesetfoldername = "Subject Practice set";
            _courseManager = new CourseManager();
        }

        private bool _SaveExamSet(Course course, Set examquestionSet)
        {
            try
            {
                //Checking or creating folder
                string coursefoldername = course.name + "_" + course.id;
                string filename = "Set" + " " + examquestionSet.setNumber.ToString() + ".json";
                string examsetfolder = Path.Combine(_directory, coursefoldername, _examsetfoldername);
                if(!Directory.Exists(examsetfolder))
                {
                    Directory.CreateDirectory(examsetfolder);
                }
                string examsetpath=Path.Combine(examsetfolder, filename);
                //Serialize
                using (FileStream fs = File.Create(examsetpath))
                {
                    string json = JsonSerializer.Serialize(examquestionSet);
                    byte[] data = new UTF8Encoding().GetBytes(json);

                    fs.Write(data, 0, data.Length);
                    System.Console.WriteLine("Saved to: \n" + examsetpath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool _SavePracticeSet(Course course, Set practicequestionSet)
        {
            try
            {
                //Checking or creating folder
                string coursefoldername = course.name + "_" + course.id;
                string filename = "Set" + " " + practicequestionSet.setNumber.ToString() + ".json";
                string examsetpath = Path.Combine(_directory, coursefoldername, _practicesetfoldername, filename);
                if (Directory.Exists(examsetpath))
                {
                    Directory.Delete(examsetpath);
                }

                //Serialize
                using (FileStream fs = File.Create(examsetpath))
                {
                    string json = JsonSerializer.Serialize(practicequestionSet);
                    byte[] data = new UTF8Encoding().GetBytes(json);

                    fs.Write(data, 0, data.Length);
                    System.Console.WriteLine("Saved to: \n" + examsetpath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool _SaveSubjectPracticeSet(Course course, string subjectid, Set practicequestionSet)
        {
            try
            {
                //Checking or creating folder
                var subjectidandnamepair = _courseManager.GetSubjectIdandNamePairs(course.id);
                var subjectname = subjectidandnamepair.First(x => x.Key == subjectid).Value;
                string subjectfoldername = subjectname + "_" + subjectid;
                string coursefoldername = course.name + "_" + course.id;
                string filename = "Set" + " " + practicequestionSet.setNumber.ToString() + ".json";
                string setsfolderpath = Path.Combine(_directory, coursefoldername, _subjectpracticesetfoldername, subjectfoldername); 
                if (!Directory.Exists(setsfolderpath))
                {
                    Directory.CreateDirectory(setsfolderpath);
                }
                string filepath=Path.Combine(setsfolderpath, filename);
                //Serialize
                using (FileStream fs = File.Create(filepath))
                {
                    string json = JsonSerializer.Serialize(practicequestionSet);
                    byte[] data = new UTF8Encoding().GetBytes(json);

                    fs.Write(data, 0, data.Length);
                    System.Console.WriteLine("Saved to: \n" + setsfolderpath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IList<Set>? GetAllExamSet(string courseid)
        {
            IList<Set>? result = new List<Set>();
            //Read filename and fileid from folder
            string? coursepath = null;
            var folders=Directory.GetDirectories(_directory);
            foreach (var folder in folders)
            {
                var items = folder.Split("_");
                var name=items[0];
                var id=items[1];
                if(id.Equals(courseid))
                {
                    coursepath = Path.Combine(_directory, folder);
                    break;
                }                
            }
            if (coursepath == null) return null;
            folders = Directory.GetDirectories(coursepath);
            foreach(var folder in folders)
            {
                if(Path.GetFileName(folder).Equals(_examsetfoldername))
                {
                    var examsetfolder = folder;
                    if (examsetfolder != null)
                    {
                        var examsetfolderpath = Path.Combine(coursepath, examsetfolder);
                        var setfiles = Directory.GetFiles(examsetfolderpath);
                        foreach (var setfile in setfiles)
                        {
                            string filedata = File.ReadAllText(setfile);
                            if (String.IsNullOrEmpty(filedata))
                            {
                                var filename = Path.GetFileName(setfile);
                                System.Console.WriteLine(filename + "is empty.");
                                continue;
                            }
                            try
                            {
                                Set? questionset = null;
                                if (!String.IsNullOrEmpty(filedata))
                                {
                                    questionset = JsonSerializer.Deserialize<Set>(filedata);
                                }
                                result.Add(questionset);
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex.ToString());
                                return null;
                            }

                        }
                    }
                }
            }
            
            return result;
        }

        public IList<Set> GetAllPracticeSet(string courseid)
        {
            throw new NotImplementedException();
        }

        public IList<Set> GetAllSubjectPracticeSet(string courseid, string subjectid)
        {
            throw new NotImplementedException();
        }

        public bool SaveExamSet(Course course, Set examquestionSet)
        {
            return _SaveExamSet(course, examquestionSet);
        }

        public bool SaveExamSets(Course course, IList<Set> examquestionSets)
        {
            int count=0;
            foreach(Set examset in examquestionSets)
            {
                if(_SaveExamSet(course, examset))
                {
                    count++;
                }
            }
            if(count==examquestionSets.Count)
            {
                return true;
            }
            return false;
        }

        public bool SavePracticeSet(Course course, Set practicequestionset)
        {
            return _SavePracticeSet(course, practicequestionset);
        }

        public bool SavePracticeSets(Course course, IList<Set> practicequestionsets)
        {
            int count = 0;
            foreach (Set examset in practicequestionsets)
            {
                if (_SavePracticeSet(course, examset))
                {
                    count++;
                }
            }
            if (count == practicequestionsets.Count)
            {
                return true;
            }
            return false;
        }

        public bool SaveSubjectPracticeSet(Course course, string subjectid, Set practicequestionset)
        {
            return _SaveSubjectPracticeSet(course,subjectid, practicequestionset);
        }

        public bool SaveSubjectPracticeSets(Course course, string subjectid, IList<Set> practicequestionsets)
        {
            int count = 0;
            foreach (Set examset in practicequestionsets)
            {
                if (_SaveSubjectPracticeSet(course,subjectid, examset))
                {
                    count++;
                }
            }
            if (count == practicequestionsets.Count)
            {
                return true;
            }
            return false;
        }
    }
}
