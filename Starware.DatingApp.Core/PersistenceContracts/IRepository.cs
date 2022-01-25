using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        T GetById (int id);
        int Update(T entity);
        bool Delete(T entity);
        int Insert(T entity);
        
    }
}
