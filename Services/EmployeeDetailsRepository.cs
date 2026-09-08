using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using PersonalDetailsAssistant.Models;
using PersonalDetailsAssistant.Options;

namespace PersonalDetailsAssistant.Services
{
    public class EmployeeDetailsRepository : IEmployeeDetailsRepository
    {
        private readonly Container _employeesContainer;
        private readonly Container _employeeDetailsContainer;

        public EmployeeDetailsRepository(CosmosClient cosmosClient, IOptions<CosmosDbOptions> options)
        {
            var config = options.Value;
            _employeesContainer = cosmosClient.GetContainer(config.DatabaseName, config.EmployeesContainerName);
            _employeeDetailsContainer = cosmosClient.GetContainer(config.DatabaseName, config.EmployeeDetailsContainerName);
        }

        public async Task<Employee?> GetEmployeeAsync(string employeeId)
        {
            try
            {
                var response = await _employeesContainer.ReadItemAsync<Employee>(employeeId, new PartitionKey(employeeId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<EmployeeDetails?> GetEmployeeDetailsAsync(string employeeId)
        {
            try
            {
                var response = await _employeeDetailsContainer.ReadItemAsync<EmployeeDetails>(employeeId, new PartitionKey(employeeId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateAddressAsync(string employeeId, AddressType addressType, string newAddress)
        {
            var details = await GetEmployeeDetailsAsync(employeeId)
                ?? throw new InvalidOperationException($"Employee details not found for '{employeeId}'.");

            switch (addressType)
            {
                case AddressType.Office:
                    details.OfficeAddress = newAddress;
                    break;
                case AddressType.Permanent:
                    details.PermanentAddress = newAddress;
                    break;
                case AddressType.Temporary:
                    details.TemporaryAddress = newAddress;
                    break;
            }

            details.LastUpdatedUtc = DateTime.UtcNow.ToString("O");
            await _employeeDetailsContainer.UpsertItemAsync(details, new PartitionKey(employeeId));
        }

        public async Task UpdateDependentsAsync(string employeeId, List<Dependent> dependents)
        {
            var details = await GetEmployeeDetailsAsync(employeeId)
                ?? throw new InvalidOperationException($"Employee details not found for '{employeeId}'.");

            details.DependentDetails = dependents;
            details.LastUpdatedUtc = DateTime.UtcNow.ToString("O");
            await _employeeDetailsContainer.UpsertItemAsync(details, new PartitionKey(employeeId));
        }

        public async Task UpdateBankAccountAsync(string employeeId, BankAccount bankAccount)
        {
            var details = await GetEmployeeDetailsAsync(employeeId)
                ?? throw new InvalidOperationException($"Employee details not found for '{employeeId}'.");

            details.BankAccount = bankAccount;
            details.LastUpdatedUtc = DateTime.UtcNow.ToString("O");
            await _employeeDetailsContainer.UpsertItemAsync(details, new PartitionKey(employeeId));
        }
    }
}
