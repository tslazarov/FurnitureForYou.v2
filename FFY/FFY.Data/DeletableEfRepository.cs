using FFY.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SoftDelete(T entity)
        {
            //TODO: Check if behaves properly
            entity.IsDeleted = true;
            base.Update(entity);
        }
    }
}
