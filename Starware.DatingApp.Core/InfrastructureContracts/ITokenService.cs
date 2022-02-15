using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.InfrastructureContracts
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
