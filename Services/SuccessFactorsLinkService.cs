using Microsoft.Extensions.Options;
using PersonalDetailsAssistant.Models;
using PersonalDetailsAssistant.Options;

namespace PersonalDetailsAssistant.Services
{
    // Builds an SSO deep link only; the actual data update always happens in SuccessFactors, never here.
    public class SuccessFactorsLinkService : ISuccessFactorsLinkService
    {
        private readonly PersonalDetailsOptions _options;

        public SuccessFactorsLinkService(IOptions<PersonalDetailsOptions> options)
        {
            _options = options.Value;
        }

        public string BuildProfileLink(PersonalDetailIntent intent)
        {
            var baseUrl = _options.SuccessFactorsBaseUrl.TrimEnd('/');
            return $"{baseUrl}?section={intent}";
        }
    }
}
