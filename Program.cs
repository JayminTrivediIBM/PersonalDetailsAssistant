using PersonalDetailsAssistant.Mcp;
using PersonalDetailsAssistant.Options;
using PersonalDetailsAssistant.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<PersonalDetailsOptions>(builder.Configuration.GetSection(PersonalDetailsOptions.SectionName));
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IKnowledgeArticleService, KnowledgeArticleService>();
builder.Services.AddSingleton<ISuccessFactorsLinkService, SuccessFactorsLinkService>();
builder.Services.AddSingleton<IWorkingHoursService, WorkingHoursService>();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<PersonalDetailsTools>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapMcp("/mcp");

app.Run();
