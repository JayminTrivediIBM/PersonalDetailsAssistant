using System.Text.Json.Serialization;

namespace PersonalDetailsAssistant.Models
{
    public class EmployeeDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string WorkLocation { get; set; } = string.Empty;
        public string OfficeAddress { get; set; } = string.Empty;
        public string PermanentAddress { get; set; } = string.Empty;
        public string TemporaryAddress { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public List<Dependent> DependentDetails { get; set; } = new();
        public string WorkDepartment { get; set; } = string.Empty;
        public string ProjectRole { get; set; } = string.Empty;
        public int TotalLeave { get; set; }
        public BankAccount BankAccount { get; set; } = new();
        public string LastUpdatedUtc { get; set; } = string.Empty;
    }
}
