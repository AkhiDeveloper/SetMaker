using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class Course
    {
        public Course()
        {
            books = new List<Book>();
            id=String.Empty;
            name=String.Empty;
        }
        public string id { get; set; }
        public string name { get; set; }
        public IList<Book> books { get; set; }
    }
}
