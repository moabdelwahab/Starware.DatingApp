using AutoMapper;
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
        private readonly IMapper mapper;
        private IUserRepository userRepository;
        private ILikeRepository likeRepository;
        private IMessageRepository messageRepository;


        public UnitOfWork(DatingAppContext context ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IUserRepository UserRepository {
            get
            {
                if(this.userRepository == null)
                {
                    this.userRepository = new UserRepository(context, mapper);
                }
                return this.userRepository;
            }
        }
        public ILikeRepository LikeRepository
        {
            get
            {
                if (this.likeRepository == null)
                {
                    this.likeRepository = new LikeRepository(context);
                }
                return this.likeRepository;
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                if (this.messageRepository == null)
                {
                    this.messageRepository = new MessageRepository(context,mapper);
                }
                return this.messageRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
