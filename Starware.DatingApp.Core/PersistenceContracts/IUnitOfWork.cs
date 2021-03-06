using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public ILikeRepository LikeRepository { get; }
        public IMessageRepository MessageRepository { get; }

        public Task SaveAsync();


    }
}
