using Microsoft.Extensions.Options;
using PersonalDetailsAssistant.Models;
using PersonalDetailsAssistant.Options;

namespace PersonalDetailsAssistant.Services
{
    public class KnowledgeArticleService : IKnowledgeArticleService
    {
        private readonly PersonalDetailsOptions _options;

        public KnowledgeArticleService(IOptions<PersonalDetailsOptions> options)
        {
            _options = options.Value;
        }

        public KnowledgeArticle? GetArticle(PersonalDetailIntent intent)
        {
            if (intent == PersonalDetailIntent.Unknown)
            {
                return null;
            }

            if (!_options.KnowledgeArticles.TryGetValue(intent.ToString(), out var article))
            {
                return null;
            }

            return new KnowledgeArticle
            {
                Title = article.Title,
                Url = article.Url,
                Summary = article.Summary
            };
        }
    }
}
