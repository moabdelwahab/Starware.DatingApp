using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.PersistenceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;

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
        public bool Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<T> GetAll()
        {
            return Entites.AsEnumerable();
        }

        public T GetById(int id)
        {
            return Entites.SingleOrDefault(x => x.Id == id);
        }

        public int Insert(T entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.CreatedBy= "User";
            context.Add(entity); 
            return entity.Id;
        }

        public int Update(T entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            entity.LastModifiedBy = "User";
            context.Update(entity);
            context.SaveChanges();
            return entity.Id;
        }
    }
}
