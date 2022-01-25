using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.PersistenceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        private IUserRepository userRepository;

        public UnitOfWork(DbContext context) => this.context = context;
        
        public IUserRepository UserRepository { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
        
    }
}
