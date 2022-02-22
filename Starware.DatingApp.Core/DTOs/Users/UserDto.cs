using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.DTOs.Users
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Token { get; set; }
    }
}
