using System;

namespace FFY.Providers.Contracts
{
    public interface ICachingProvider
    {
        void AddItem(string key, object value, DateTime expirationDateTime);

        void InsertItem(string key, object value);

        object GetItem(string key);
    }
}
