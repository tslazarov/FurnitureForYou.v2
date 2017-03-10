using System;

namespace FFY.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}