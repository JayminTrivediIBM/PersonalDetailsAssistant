using Azure.Identity;
using Microsoft.Azure.Cosmos;
using PersonalDetailsAssistant.Mcp;
using PersonalDetailsAssistant.Options;
using PersonalDetailsAssistant.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<PersonalDetailsOptions>(builder.Configuration.GetSection(PersonalDetailsOptions.SectionName));
builder.Services.Configure<CosmosDbOptions>(builder.Configuration.GetSection(CosmosDbOptions.SectionName));
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IKnowledgeArticleService, KnowledgeArticleService>();
builder.Services.AddSingleton<ISuccessFactorsLinkService, SuccessFactorsLinkService>();
builder.Services.AddSingleton<IWorkingHoursService, WorkingHoursService>();

// Authenticates via the Container App's managed identity; no keys/secrets are stored anywhere.
builder.Services.AddSingleton(sp =>
{
    var cosmosOptions = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<CosmosDbOptions>>().Value;
    var clientOptions = new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase }
    };
    return new CosmosClient(cosmosOptions.AccountEndpoint, new DefaultAzureCredential(), clientOptions);
});
builder.Services.AddSingleton<IEmployeeDetailsRepository, EmployeeDetailsRepository>();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<PersonalDetailsTools>()
    .WithTools<EmployeeDetailsTools>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapMcp("/mcp");

app.Run();
