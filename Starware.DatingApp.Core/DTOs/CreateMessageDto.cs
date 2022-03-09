using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.DTOs
{
    public class CreateMessageDto
    {
        public string Content { get; set; }
        public string RecipientUsername { get; set; }
    }
}
