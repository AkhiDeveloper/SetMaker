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

        private bool _HasBook(Course cousre, string bookid)
        {
            if(cousre == null || String.IsNullOrEmpty(bookid))
            {
                return false;
            }
            if(cousre.books.Any(x=>x.id == bookid))
            {
                return true;
            }
            return false;
        }
        public bool AddBook(Course course, Book book)
        {
            if(course == null || book == null)
            {
                return false;
            }
            if(!_HasBook(course, book.id))
            {
                course.books.Add(book);
                return true;
            }
            return false;
        }

        public Course? GetCourse(string id)
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

        public IList<Question>? GetQuestions(string courseid)
        {
            Course? course = GetCourse(courseid);
            if (course == null)
            {
                return null;
            }
            IList<Question>? result = new List<Question>();
            foreach(var book in course.books)
            {
                if(book== null)
                {
                    continue;
                }
                foreach(var subject in book.subjects)
                {
                    if (subject == null) continue;
                    foreach(var question in subject.questions)
                    {
                        result.Add(question);
                    }
                }
            }
            return result;
        }

        public IList<Question>? GetQuestions(string courseid, string subjectid)
        {
            Course? course= GetCourse(courseid);
            if(course == null)
            {
                return null;
            }
            IList<Question> result= new List<Question>();
            foreach(var book in course.books)
            {
                if (book == null) continue;
                foreach(var subject in book.subjects)
                {
                    if(subject == null) continue;
                    if(subject.id == subjectid)
                    {
                        try
                        {
                            foreach (var question in subject.questions)
                            {
                                result.Add(question);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                            continue;
                        }
                        
                    }
                }
            }
            return result;
        }

        public IDictionary<string, string>? GetSubjectIdandNamePairs(string courseid)
        {
            try
            {
                Course? course = GetCourse(courseid);
                if (course == null) return null;
                IDictionary<string, string> result = new Dictionary<string, string>();
                foreach (var book in course.books)
                {
                    if (book == null) continue;
                    foreach (var subject in book.subjects)
                    {
                        if (subject == null) continue;
                        try
                        {
                            if (!result.Any(x => x.Key==subject.id))
                            {

                                result.Add(subject.id, subject.name);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex);
                        }
                        
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public IList<string>? GetSubjectsId(string courseid)
        {
            Course? course= GetCourse(courseid);
            if(course == null) return null;
            IList<string> result= new List<string>();   
            foreach(var book in course.books)
            {
                if(book == null) continue;
                foreach(var subject in book.subjects)
                {
                    if(subject==null) continue;
                    if (!result.Any(x => x.Equals(subject.id)))
                    {
                        result.Add(subject.id);
                    }
                }
            }
            return result;
        }

        public bool HasSubject(Course course, string subjectcode)
        {
            if(course == null) return false;
            if(subjectcode == null) return false;
            foreach(var book in course.books)
            {
                if (book.subjects.Any(x => x.id == subjectcode))
                    return true;
            }
            return false;
        }

        public bool SaveCourse(Course course)
        {
            if(course==null) return false;
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

        public bool ForceAddBook(Course course, Book book)
        {
            if(course == null || book == null) return false;

            if(!_HasBook(course,book.id))
            {
                var existingbooks = course.books.Where(x => x.id == book.id);
                if (existingbooks != null)
                {
                    foreach(Book existingbook in existingbooks)
                    {
                        course.books.Remove(existingbook);
                    }
                    
                }
            }
            course.books.Add(book);
            return true;
        }
    }
}
