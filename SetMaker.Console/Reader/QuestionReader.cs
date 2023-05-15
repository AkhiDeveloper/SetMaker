using SetMaker.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetMaker.Console;

namespace SetMaker.Console.Reader
{
    public class QuestionReader : IQuestionReader
    {
        public ICollection<Question>? AssignCorrectOptiontoQuestions(ICollection<Question>? questions, IDictionary<int, string> correctOptions)
        {
            if (questions != null || correctOptions != null)
            {
                
                foreach (Question question in questions)
                {
                    foreach (var correctoption in correctOptions)
                    {
                        if (question.questionnumber == correctoption.Key)
                        {
                            foreach (var option in question.options)
                            {
                                if (option.key.ToLower() == correctoption.Value.ToLower())
                                {
                                    option.isCorrect = true;
                                }
                            }
                        }
                    }
                }
            }
            return questions;
        }

        public IDictionary<int, string> ReadCorrectOptionsfromfile(string filepath)
        {
            IDictionary<int, string> correctoptions = new Dictionary<int, string>();
            using (StreamReader file = new StreamReader(filepath))
            {
                var lines = file.ReadToEnd().Split(';');
                int flag = 0;
                string? qsnnumber = null;
                string? optsymbol = null;
                foreach (string line in lines)
                {
                    foreach (var item in line)
                    {
                        if (Char.IsDigit(item))
                        {
                            if (flag == 1)
                            {
                                qsnnumber = null;
                            }
                            qsnnumber = qsnnumber + item;
                            flag = 0;

                        }
                        if (!Char.IsDigit(item))
                        {
                            flag = 1;
                        }
                        if (Char.IsLetter(item))
                        {
                            optsymbol = item.ToString();
                            try
                            {
                                if (item != 0 && qsnnumber!=null)
                                {
                                    correctoptions.Add(int.Parse(qsnnumber), optsymbol);
                                }

                            }
                            catch (Exception ex)
                            {                                
                                continue;
                            }
                            qsnnumber = null;
                        }
                    }
                }
            }
            return correctoptions;
        }

        public ICollection<Question>? ReadQuestionsfromfile(string filepath)
        {
            if (filepath == null)
            {
                return null;
            }
            ICollection<Question> result = new List<Question>();

            var lines = File.ReadAllLines(filepath);
            if (lines == null)
            {
                return null;
            }
            IList<String> Formatedlines=new List<String>();
            foreach(var line in lines)
            {
                if(String.IsNullOrEmpty(line))
                {
                    continue;
                }
                Formatedlines.Add(FormatString(line));
            }
            lines= Formatedlines.ToArray();
            Question? question = null;
            for (int i = 0; i < lines.Length; i++)
            {

                int? number;
                string body;
                string line = lines[i].Trim();
                var sucess = CustomString.TrySeperateStartNumberAndLetters(line, out number, out body);
                if (string.IsNullOrEmpty(body))
                {
                    continue;
                }
                body = body.Trim();
                if (sucess == true)
                {
                    if (question == null)
                    {
                        question = new Question()
                        { questionnumber=number,
                        body=body};
                        continue;
                    }
                    if (question != null)
                    {
                        result.Add(question);
                        question = new Question()
                        { questionnumber=number,
                        body=body};
                        continue;
                    }
                }
                if(question != null)
                {
                    if (sucess == false)
                    {
                        var tag = line.GetOpeningTag();
                        if(tag.ToLower() == "img")
                        {
                            question.ImageUrl = line.GetTagText("img");
                        }
                        else
                        {
                            try
                            {
                                Option option = new Option();
                                var optionbody = CustomString.TrimSymbolStart(body).ToString();
                                var optionkey = optionbody[0].ToString();
                                var optionValue = optionbody.Substring(1);
                                var optionImageTagText = optionValue.GetTagText("img");
                                option.key = optionkey;
                                if (string.IsNullOrEmpty(optionImageTagText))
                                {
                                    option.body = CustomString.TrimSymbolStart(optionValue);
                                }
                                else
                                {
                                    option.ImageUrl = optionImageTagText;
                                }
                                if (option != null && question != null)
                                {
                                    question.options.Add(option);
                                }
                            }
                            catch
                            {

                            }
                        }
                        

                    }
                }
                

            }
            if(question!=null)
            {
                question.options = question.options.OrderBy(x => x.key).ToList();
                result.Add(question);
            }
            return result.OrderBy(x => x.questionnumber).ToList();
        }

        private string FormatString(string text)
        {
            string result_text = null;
            bool formatter_flag = false;
            foreach (var word in text)
            {
                if (word == 92)
                {
                    formatter_flag = true;
                    continue;
                }
                if (formatter_flag == true)
                {
                    if (word == 110)
                    {
                        result_text = result_text + "\n";
                    }
                    if (word == 116)
                    {
                        result_text = result_text + "\t";
                    }
                    formatter_flag = false;
                    continue;
                }
                result_text = result_text + word;
            }
            return result_text;
        }
    }
}
