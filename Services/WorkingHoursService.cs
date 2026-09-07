using Microsoft.Extensions.Options;
using PersonalDetailsAssistant.Options;

namespace PersonalDetailsAssistant.Services
{
    public class WorkingHoursService : IWorkingHoursService
    {
        private readonly WorkingHoursOptions _options;
        private readonly TimeProvider _timeProvider;

        public WorkingHoursService(IOptions<PersonalDetailsOptions> options, TimeProvider timeProvider)
        {
            _options = options.Value.WorkingHours;
            _timeProvider = timeProvider;
        }

        public bool IsWithinWorkingHours()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(_options.TimeZoneId);
            var localNow = TimeZoneInfo.ConvertTime(_timeProvider.GetUtcNow(), timeZone);

            if (localNow.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return false;
            }

            return localNow.Hour >= _options.StartHour && localNow.Hour < _options.EndHour;
        }
    }
}
