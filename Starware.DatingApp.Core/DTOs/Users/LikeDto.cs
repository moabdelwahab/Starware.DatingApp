using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.DTOs.Users
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string KnowAs { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime LastActive { get; set; }
        public string Username { get; set; }
    }
}
