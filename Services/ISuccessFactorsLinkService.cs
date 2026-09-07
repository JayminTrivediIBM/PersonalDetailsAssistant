using PersonalDetailsAssistant.Models;

namespace PersonalDetailsAssistant.Services
{
    public interface ISuccessFactorsLinkService
    {
        string BuildProfileLink(PersonalDetailIntent intent);
    }
}
