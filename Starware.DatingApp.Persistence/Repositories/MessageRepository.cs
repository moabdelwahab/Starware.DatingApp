using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Persistence.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DatingAppContext context;
        private readonly IMapper mapper;

        public MessageRepository(DatingAppContext context,IMapper mapper) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int SenderId, int recipientId)
        {
            return await context.Message.Where(message =>
            message.SenderId == SenderId
          && message.RecipientId == recipientId
          || message.SenderId == recipientId
          && message.RecipientId == SenderId
           ).OrderBy(m => m.CreationDate).ToListAsync();
        }

        public async Task<PagedList<MessageDto>> GetUserMessage(GetUserMessagesRequest getUserMessagesRequest)
        {

            var query = context.Message.OrderByDescending(message => message.CreationDate)
                .AsQueryable();

            query = getUserMessagesRequest.Container switch
            {
                "Inbox" => query.Where(message => message.RecipientUsername == getUserMessagesRequest.Username),
                "Outbox" => query.Where(message => message.SenderUsername == getUserMessagesRequest.Username),
                _ => query.Where(message => message.RecipientUsername == getUserMessagesRequest.Username && message.DateReaded == null)
            };

            var messagesMapped = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messagesMapped, getUserMessagesRequest.PageNumber,
                getUserMessagesRequest.PageSize);


        }
    }
}
