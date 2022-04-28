using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class CourseManager : ICourseManager
    {
        private string _directory;
        public CourseManager()
        {
            _directory = @"D:\Downloads\Courses";
            if(!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
        }
        public Course GetCourse(string id)
        {
            try
            {
                string[] files = Directory.GetFiles(_directory);
                foreach (string file in files)
                {
                    string filedata = File.ReadAllText(file);
                    if (String.IsNullOrEmpty(filedata))
                    {
                        var filename = Path.GetFileName(file);
                        System.Console.WriteLine(filename + "is empty.");
                        continue;
                    }
                    try
                    {
                        Course? course = null;
                        if (!String.IsNullOrEmpty(filedata))
                        {
                            course = JsonSerializer.Deserialize<Course>(filedata);
                        }

                        if (course != null)
                        {
                            if (course.id == id)
                            {
                                System.Console.WriteLine("Course found.....!\n" + "Code=" + course.id + "\nName=" + course.name);
                                return course;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        continue;
                    }


                }
                System.Console.WriteLine("Book Not Found.......!");
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IList<Question> GetQuestions(string courseid)
        {
            throw new NotImplementedException();
        }

        public IList<Question> GetQuestions(string courseid, string subjectid)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetSubjectsId(string courseid)
        {
            throw new NotImplementedException();
        }

        public bool SaveCourse(Course course)
        {
            string filename = course.name + "_" + course.id + ".json";
            if (_directory == null)
            {
                System.Console.WriteLine("Invalid Directory!");
                System.Console.ReadKey();
                return false;
            }
            string filepath = Path.Combine(_directory, filename);
            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                using (FileStream fs = File.Create(filepath))
                {
                    string json = JsonSerializer.Serialize(course);
                    byte[] data = new UTF8Encoding().GetBytes(json);

                    fs.Write(data, 0, data.Length);
                    System.Console.WriteLine("Saved to: \n" + filepath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
