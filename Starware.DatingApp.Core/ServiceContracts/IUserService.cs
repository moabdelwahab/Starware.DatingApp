﻿using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllUser();
        Task<AppUser> GetById(int id);
        Task<AppUser> GetByUsername(string username);
        Task<int> AddUser(AppUser user); 
    }
}
