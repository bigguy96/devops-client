namespace AzureDevOpsWiki
{
    public class WikiDetails
    {
        public string path { get; set; }
        public int order { get; set; }
        public bool isParentPage { get; set; }
        public string gitItemPath { get; set; }
        public Subpage[] subPages { get; set; }
        public string url { get; set; }
        public string remoteUrl { get; set; }
        public string content { get; set; }
    }
}