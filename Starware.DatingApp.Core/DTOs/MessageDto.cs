using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateReaded { get; set; }
        public bool RecipientDeleted { get; set; }
        public bool SenderDeleted { get; set; }


    }
}
