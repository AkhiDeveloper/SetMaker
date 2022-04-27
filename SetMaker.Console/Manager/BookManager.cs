using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public class BookManager : IBookManager
    {
        private string? _directory;
        public BookManager()
        {
            this._directory = @"D:\Downloads\Book";
            if(!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
        }
        public Book? GetBook(string id)
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
                        Book? book = null;
                        if(!String.IsNullOrEmpty(filedata))
                        {
                            book = JsonSerializer.Deserialize<Book>(filedata);
                        }
                        
                        if (book != null)
                        {
                            if (book.id == id)
                            {
                                System.Console.WriteLine("Book found.....!\n" + "Code=" + book.id + "\nName=" + book.name );
                                return book;
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


        public Book? ReadBookfromfolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                return null;
            }

            //book naming convention Bookname_Bookcode
            //get detail of book from folder name
            var bookfilename = Path.GetFileName(folder);
            var bookelements = bookfilename.Split('_');
            var bookname = bookelements[0].Trim();
            var bookcode = bookelements[1].Trim();
            Book result = new Book() { id=bookcode, name=bookname };

            //get subject sub directives of book folder
            var subjectfolders = Directory.GetDirectories(folder);
            foreach (var subjectfolder in subjectfolders)
            {

                //naming convention "SubjectSN_SubjectName_SubjectCode"
                var filename = Path.GetFileName(subjectfolder);
                var elemnts = filename.Split('_');
                var subjectSn = elemnts[0].Trim();
                var subjectName = elemnts[1].Trim();
                var subjectCode = bookcode + "@" + elemnts[2].Trim();
                if (subjectCode == null) continue;
                Subject subject = new Subject() { id=subjectCode, name=subjectName};

                //getting question and correct option files 
                //Adding questions to subject
                Reader.IQuestionReader questionReader = new Reader.QuestionReader();
                ICollection<Question>? readedquestions = null;
                IDictionary<int, string>? readcorrectoptions = null;
                var files = Directory.GetFiles(subjectfolder);
                foreach (var file in files)
                {
                    filename = Path.GetFileNameWithoutExtension(file).Trim();
                    var check = filename.EndsWith("Q");
                    //file name ending with Q is question and A is correct option
                    if (filename.EndsWith("Q") && readedquestions == null)
                    {
                        //read question  file

                        
                            readedquestions = questionReader.ReadQuestionsfromfile(file);
                            continue;
                        
                    }
                    if (filename.EndsWith("A") && readcorrectoptions == null)
                    {
                        //read correct option  file
                        
                            readcorrectoptions = questionReader.ReadCorrectOptionsfromfile(file);
                            continue;
                        
                    }
                }
                //Assign correct option to question read
                if (readedquestions != null && readcorrectoptions != null)
                {
                    readedquestions = questionReader.AssignCorrectOptiontoQuestions(readedquestions, readcorrectoptions);
                }


                //Add Questions to subject
                foreach(Question question in readedquestions)
                {
                    subject.questions.Add(question);
                }
                
                result.subjects.Add(subject);
            }
            return result;
        }

        public bool SaveBook(Book book)
        {
            
            string filename = book.name + "_" + book.id + ".json";
            if(_directory==null)
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
                    string json = JsonSerializer.Serialize(book);
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
