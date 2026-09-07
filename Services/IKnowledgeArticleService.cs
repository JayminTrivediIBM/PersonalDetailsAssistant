using PersonalDetailsAssistant.Models;

namespace PersonalDetailsAssistant.Services
{
    public interface IKnowledgeArticleService
    {
        KnowledgeArticle? GetArticle(PersonalDetailIntent intent);
    }
}
