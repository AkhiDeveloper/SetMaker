namespace SetMaker.Console.Models
{
    public class CourseSetSetting
    {
        public CourseSetSetting()
        {
            subjectquestions=new Dictionary<string, int>();
        }
        /// <summary>
        /// key => subjectcode
        /// value => number of questions
        /// </summary>
        public IDictionary<string, int> subjectquestions { get; set; }
        public TimeSpan? completionTime { get; set; }
        public decimal? negativemarking { get; set; }
    }
}