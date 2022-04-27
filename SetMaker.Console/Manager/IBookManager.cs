using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Manager
{
    public interface IBookManager
    {
        bool SaveBook(Book book);
        Book? GetBook(string id);
        bool ReadBookfromfolder(string folder);
    }
}
