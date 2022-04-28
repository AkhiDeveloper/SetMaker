namespace SetMaker.Console.Models
{
    public class Subject
    {
        public Subject()
        {
            questions = new List<Question>();
            _id=string.Empty;
            name = string.Empty;
        }
        private string _id;

        public string id
        {
            get { return _id.ToLower(); }
            set { _id = value.ToLower(); }
        }

        public int sn { get; set; }
        public string name { get; set; }
        public IList<Question> questions { get; set; }
    }
}