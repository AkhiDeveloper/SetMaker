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


        public bool ReadBookfromfolder(string folder)
        {
            throw new NotImplementedException();
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
