using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class QuestionSet
    {
        public QuestionSet()
        {
            questions=new List<SetQuestion>();
        }
        public string topicCode { get; set; }
        public string topicName { get; set; }
        public string id { get; set; }
        public int setNumber { get; set; }
        public DateTime createdDate { get; set; }
        public TimeSpan completionTime { get; set; }
        public decimal negativeMarking { get; set; }
        public bool IsTimeRestrictionApplied { get; set; }
        public IList<SetQuestion> questions { get; set; }
    }
}
