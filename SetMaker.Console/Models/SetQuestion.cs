namespace SetMaker.Console.Models
{
    public class SetQuestion
    {
        public Question question { get; set; }
        public TimeSpan CompletionTime { get; set; }
        public decimal Weightage { get; set; }
    }
}