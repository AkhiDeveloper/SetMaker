using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class Set
    {
        public Set()
        {
            questions=new List<Question>();
        }
        public string topicName { get; set; }
        public string id { get; set; }
        public int setNumber { get; set; }
        public DateTime createdDate { get; set; }
        public TimeSpan? completionTime { get; set; }
        public decimal? negativeMarking { get; set; }
        public bool IsTimeRestrictionApplied { get; set; }
        public IList<Question> questions { get; set; }
    }
}
