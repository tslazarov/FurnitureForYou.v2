using FFY.Providers.Contracts;
using System;

namespace FFY.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
