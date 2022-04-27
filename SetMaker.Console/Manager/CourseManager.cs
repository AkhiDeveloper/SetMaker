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

        public bool Savecourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
