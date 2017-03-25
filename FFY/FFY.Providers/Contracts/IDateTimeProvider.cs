using System;

namespace FFY.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentTime();
    }
}
