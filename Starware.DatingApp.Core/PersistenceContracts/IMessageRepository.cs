using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface IMessageRepository : IRepository<Message> 
    {
        Task<IEnumerable<Message>> GetMessageThread(int SenderId,int recipientId);
        Task<PagedList<MessageDto>> GetUserMessage(GetUserMessagesRequest getUserMessagesRequest);
        
    }
}
