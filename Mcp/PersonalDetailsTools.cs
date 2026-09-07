using System.ComponentModel;
using ModelContextProtocol.Server;
using PersonalDetailsAssistant.Models;
using PersonalDetailsAssistant.Services;

namespace PersonalDetailsAssistant.Mcp
{
    [McpServerToolType]
    public class PersonalDetailsTools
    {
        private readonly IKnowledgeArticleService _articleService;
        private readonly ISuccessFactorsLinkService _linkService;
        private readonly IWorkingHoursService _workingHoursService;

        public PersonalDetailsTools(
            IKnowledgeArticleService articleService,
            ISuccessFactorsLinkService linkService,
            IWorkingHoursService workingHoursService)
        {
            _articleService = articleService;
            _linkService = linkService;
            _workingHoursService = workingHoursService;
        }

        [McpServerTool(Name = "get_knowledge_article"), Description("Gets the knowledge article for a personal details update intent (Address, Dependents, or BankAccount).")]
        public KnowledgeArticle? GetKnowledgeArticle(
            [Description("The update intent: Address, Dependents, or BankAccount")] PersonalDetailIntent intent)
        {
            return _articleService.GetArticle(intent);
        }

        [McpServerTool(Name = "get_sso_link"), Description("Gets the SuccessFactors SSO deep link for a personal details update intent. Only call this after the colleague has explicitly confirmed they want to update now.")]
        public string GetSsoLink(
            [Description("The update intent: Address, Dependents, or BankAccount")] PersonalDetailIntent intent)
        {
            return _linkService.BuildProfileLink(intent);
        }

        [McpServerTool(Name = "check_working_hours"), Description("Checks whether it is currently within working hours, to decide whether to offer a live agent or case-only escalation.")]
        public bool CheckWorkingHours()
        {
            return _workingHoursService.IsWithinWorkingHours();
        }
    }
}
