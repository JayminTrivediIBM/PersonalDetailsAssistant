using PersonalDetailsAssistant.Models;

namespace PersonalDetailsAssistant.Services
{
    public interface IEmployeeDetailsRepository
    {
        Task<Employee?> GetEmployeeAsync(string employeeId);
        Task<EmployeeDetails?> GetEmployeeDetailsAsync(string employeeId);
        Task UpdateAddressAsync(string employeeId, AddressType addressType, string newAddress);
        Task UpdateDependentsAsync(string employeeId, List<Dependent> dependents);
        Task UpdateBankAccountAsync(string employeeId, BankAccount bankAccount);
    }
}
