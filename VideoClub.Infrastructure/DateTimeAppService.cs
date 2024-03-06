using VideoClub.Contracts.Interfaces;

namespace VideoClub.Infrastructure
{
    public class DateTimeAppService : DateTimeService
    {
      
        DateTime DateTimeService.Now()
        {
           return DateTime.Now;
        }
    }
}
