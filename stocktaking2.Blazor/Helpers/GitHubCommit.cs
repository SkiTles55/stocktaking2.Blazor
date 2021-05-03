namespace stocktaking2.Blazor.Helpers
{
    public class GitHubCommit
    {
        public string html_url { get; set; }
        public Commit commit { get; set; }
    }

    public class Commit
    {
        public Author author { get; set; }
        public string message { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string date { get; set; }
    }
}