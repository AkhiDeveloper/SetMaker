using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class Book
    {
        public Book()
        {
            subjects =new List<Subject>();
            name=String.Empty;
            id=String.Empty;
        }
        public string id { get; set; }
        public string name { get; set; }
        public IList<Subject> subjects { get; set; }
    }
}
