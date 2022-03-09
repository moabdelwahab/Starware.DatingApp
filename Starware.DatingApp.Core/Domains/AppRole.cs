using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.Domains
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> RoleUsers { get; set; }
    }
}
