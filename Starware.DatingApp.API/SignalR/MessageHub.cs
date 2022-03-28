using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Starware.DatingApp.API.Extensions;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;

namespace Starware.DatingApp.API.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IUserService userService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageHub(IUserService userService,
                          IUnitOfWork unitOfWork,
                          IMapper mapper)
        {
            this.userService = userService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserName(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var messages = await this.userService.GetMessageThread(Context.User.GetUserName(), otherUser);
            await Clients.Group(groupName).SendAsync("ReciveMessageThread",messages.Data);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddMessage(CreateMessageDto messageDto)
        {

            var user = await unitOfWork.UserRepository.GetByUserName(Context.User.GetUserName());
            var recipet = await unitOfWork.UserRepository.GetByUserName(messageDto.RecipientUsername);

            if(user == null ) throw new HubException();

            if (recipet == null) throw new HubException();

            Message message = new Message();
            message.RecipientId = recipet.Id;
            message.Sender = user;
            message.SenderId = user.Id;
            message.Content = messageDto.Content;
            message.CreatedBy = user.UserName;
            message.RecipientUsername = recipet.UserName;
            message.Recipient = recipet;
            message.SenderUsername = user.UserName;
            message.CreationDate = DateTime.Now;
            await unitOfWork.MessageRepository.Insert(message);

            await Clients.Groups(GetGroupName(user.UserName,recipet.UserName)).SendAsync("NewMessage",mapper.Map<MessageDto>(message));

        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}
