namespace DuolingoNou.Models
{
    public class Achievement
    {
        public string Title { get; set; }
        public double Progress { get; set; }
        public string CompletionStatus { get; set; }

        // Constructor to initialize properties
        public Achievement(string title, double progress, string completionStatus)
        {
            Title = title;
            Progress = progress;
            CompletionStatus = completionStatus;
        }
    }
}
