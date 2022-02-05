using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.PersistenceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using System.Linq.Expressions;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbContext context;
        private DbSet<T> Entites { get; set; }


        public Repository(DbContext context)
        {
            this.context = context;
            this.Entites = context.Set<T>();
        }
        public async Task<bool> Delete(T entity)
        {
          context.Remove(entity);
          return await context.SaveChangesAsync() > 0 ;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entites.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await Entites.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Insert(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreatedBy= "User";
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(T entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            entity.LastModifiedBy = "User";
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
