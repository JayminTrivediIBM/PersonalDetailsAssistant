using System.ComponentModel;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Server;
using PersonalDetailsAssistant.Models;
using PersonalDetailsAssistant.Options;
using PersonalDetailsAssistant.Services;

namespace PersonalDetailsAssistant.Mcp
{
    [McpServerToolType]
    public class EmployeeDetailsTools
    {
        private readonly IEmployeeDetailsRepository _repository;
        private readonly string _demoEmployeeId;

        public EmployeeDetailsTools(IEmployeeDetailsRepository repository, IOptions<CosmosDbOptions> options)
        {
            _repository = repository;
            _demoEmployeeId = options.Value.DemoEmployeeId;
        }

        [McpServerTool(Name = "get_employee_details"), Description("Gets the colleague's current employee details, including work location, addresses, marital status, dependents, department, role, and total leave.")]
        public async Task<EmployeeDetails?> GetEmployeeDetails()
        {
            return await _repository.GetEmployeeDetailsAsync(_demoEmployeeId);
        }

        [McpServerTool(Name = "update_address"), Description("Updates the colleague's office, permanent, or temporary address directly in the employee record.")]
        public async Task<string> UpdateAddress(
            [Description("Which address to update: Office, Permanent, or Temporary")] AddressType addressType,
            [Description("The new address value")] string newAddress)
        {
            await _repository.UpdateAddressAsync(_demoEmployeeId, addressType, newAddress);
            return $"{addressType} address updated successfully.";
        }

        [McpServerTool(Name = "update_dependents"), Description("Updates the colleague's dependent details directly in the employee record. Replaces the full list of dependents.")]
        public async Task<string> UpdateDependents(
            [Description("The full list of dependents to store")] List<Dependent> dependents)
        {
            await _repository.UpdateDependentsAsync(_demoEmployeeId, dependents);
            return "Dependent details updated successfully.";
        }

        [McpServerTool(Name = "update_bank_account"), Description("Updates the colleague's bank account details directly in the employee record.")]
        public async Task<string> UpdateBankAccount(
            [Description("Bank account number")] string accountNumber,
            [Description("Bank name")] string bankName,
            [Description("Bank routing/IFSC number")] string routingNumber)
        {
            await _repository.UpdateBankAccountAsync(_demoEmployeeId, new BankAccount
            {
                AccountNumber = accountNumber,
                BankName = bankName,
                RoutingNumber = routingNumber
            });
            return "Bank account details updated successfully.";
        }
    }
}
