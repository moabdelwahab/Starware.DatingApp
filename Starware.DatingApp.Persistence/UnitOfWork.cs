﻿using AutoMapper;
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
    }
}
