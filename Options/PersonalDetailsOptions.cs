namespace PersonalDetailsAssistant.Options
{
    public class PersonalDetailsOptions
    {
        public const string SectionName = "PersonalDetails";

        public string SuccessFactorsBaseUrl { get; set; } = string.Empty;
        public Dictionary<string, KnowledgeArticleOption> KnowledgeArticles { get; set; } = new();
        public WorkingHoursOptions WorkingHours { get; set; } = new();
    }

    public class KnowledgeArticleOption
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
    }

    public class WorkingHoursOptions
    {
        public int StartHour { get; set; } = 9;
        public int EndHour { get; set; } = 17;
        public string TimeZoneId { get; set; } = "UTC";
    }
}
