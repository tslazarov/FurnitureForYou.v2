using System.Linq;
using FFY.Data.Contracts;
using FFY.Models.Contracts;

namespace FFY.Data
{
    public class DeletableEfRepository<T> : EfRepository<T>, IDeletableEfRepository<T>
        where T: class, IDeletableEntity
    {
        public DeletableEfRepository(IFFYDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<T> AllWithoutDeleted()
        {
            return base.All().Where(e => !e.IsDeleted);
        }

        public void DetachEntry(T entity)
        {
            base.DetachEntry(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            base.Update(entity);
        }
    }
}
