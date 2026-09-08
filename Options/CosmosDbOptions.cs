namespace PersonalDetailsAssistant.Options
{
    public class CosmosDbOptions
    {
        public const string SectionName = "CosmosDb";

        public string AccountEndpoint { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string EmployeesContainerName { get; set; } = string.Empty;
        public string EmployeeDetailsContainerName { get; set; } = string.Empty;
        public string DemoEmployeeId { get; set; } = string.Empty;
    }
}
