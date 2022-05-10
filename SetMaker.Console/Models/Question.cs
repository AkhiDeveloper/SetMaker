using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetMaker.Console.Models
{
    public class Question
    {
        public Question()
        {
            options = new List<Option>();
            body = string.Empty;
            id=string.Empty;
            questionnumber = null;
        }

        public Question(int? questionnumber, string? body)
        {
            this.id = string.Empty;
            this.questionnumber = questionnumber;
            this.body = body == null ? String.Empty: body ;
            this.options = new List<Option>();
        }

        public string id { get; set; }
        public string? ImageUrl { get; set; }
        public int? questionnumber { get; set; }
        public string body { get; set; }
        public IList<Option> options { get; set; }

    }
}
