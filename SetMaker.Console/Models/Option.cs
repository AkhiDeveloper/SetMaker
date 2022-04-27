using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class Option
    {
        public Option()
        {
            _key=string.Empty;
            body=string.Empty;
            isCorrect=false;
        }
        
        private string _key;

        public string key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string body { get; set; }
        public bool isCorrect { get; set; }
    }
}
