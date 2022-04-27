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
        public Book GetBook(string id)
        {
            throw new NotImplementedException();
            //try
            //{
            //    string[] files = Directory.GetFiles(_directory);
            //    foreach (string file in files)
            //    {
            //        string filedata = File.ReadAllText(file);
            //        if (String.IsNullOrEmpty(filedata))
            //        {
            //            var filename = Path.GetFileName(file);
            //            System.Console.WriteLine(filename + "is empty.");
            //            continue;
            //        }
            //        try
            //        {
            //            Course course = JsonSerializer.Deserialize<Course>(filedata);
            //            if (course != null)
            //            {
            //                if (course.id == id)
            //                {
            //                    System.Console.WriteLine("Book found.....!\n" + "Code=" + course.code + "\nName=" + course.name + "\nNo. of Books=" + course.books.Count);
            //                    return course;
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            System.Console.WriteLine(ex.ToString());
            //            continue;
            //        }


            //    }
            //    System.Console.WriteLine("Book Not Found.......!");
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex.Message);
            //    return null;
            //}
        }

        public bool ReadBookfromfolder(string folder)
        {
            throw new NotImplementedException();
        }

        public bool SaveBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
