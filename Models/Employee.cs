using System.Text.Json.Serialization;

namespace PersonalDetailsAssistant.Models
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string SupervisorId { get; set; } = string.Empty;
    }
}
