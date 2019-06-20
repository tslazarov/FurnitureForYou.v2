using System.Linq;

namespace FFY.Data.Contracts
{
    public interface IDeletableEfRepository<T>
        where T : class
    {
        T GetById(object id);

        IQueryable<T> All();

        IQueryable<T> AllWithoutDeleted();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void SoftDelete(T entity);

        void DetachEntry(T entity);
    }
}
