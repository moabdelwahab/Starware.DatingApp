using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatingAppContext context;
        private IUserRepository userRepository;
        
        public UnitOfWork(DatingAppContext context)
        {
            this.context = context;
        }

        public IUserRepository UserRepository {
            get
            {
                if(this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context);
                }
                return this.userRepository;
            }
        }
    }
}
