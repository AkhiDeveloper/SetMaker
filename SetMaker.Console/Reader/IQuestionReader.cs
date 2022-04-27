using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetMaker.Console.Models;

namespace SetMaker.Console.Reader
{
    public interface IQuestionReader
    {
        ICollection<Question>? ReadQuestionsfromfile(string filepath);
        IDictionary<int, string>? ReadCorrectOptionsfromfile(string filepath);
        ICollection<Question>? AssignCorrectOptiontoQuestions(ICollection<Question>? questions, IDictionary<int, string> correctOptions);
    }
}
