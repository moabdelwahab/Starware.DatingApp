using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface IRepository<T> 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById (int id);
        Task<int> Update(T entity);
        Task<bool> Delete(T entity);
        Task<int> Insert(T entity);
        
    }
}
