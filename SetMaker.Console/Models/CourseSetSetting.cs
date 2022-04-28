namespace SetMaker.Console.Models
{
    public class CourseSetSetting
    {
        /// <summary>
        /// key => subjectcode
        /// value => number of questions
        /// </summary>
        IDictionary<string, int> subjectquestions { get; set; }
        TimeSpan completionTime { get; set; }
        decimal negativemarking { get; set; }
    }
}