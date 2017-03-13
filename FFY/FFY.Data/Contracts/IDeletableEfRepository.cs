using System.Linq;

namespace FFY.Data.Contracts
{
    public interface IDeletableEfRepository<T>
        where T : class
    {
        IQueryable<T> AllWithoutDeleted();

        void SoftDelete(T entity);
    }
}
