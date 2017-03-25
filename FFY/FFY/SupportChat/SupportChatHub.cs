using Bytes2you.Validation;
using FFY.Data.Factories;
using FFY.Services.Contracts;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace FFY.Web.SupportChat
{
    [HubName("supportChatHub")]
    public class SupportChatHub : Hub
    {
        private const string SupportRole = "Support";
        private const string ClientRole = "Client";
        private const string SupportRoomName = "support-room";

        private readonly IChatUsersService chatUsersService;
        private readonly IChatUserFactory chatUserFactory;

        public SupportChatHub() { }

        public SupportChatHub(IChatUsersService chatUsersService,
            IChatUserFactory chatUserFactory)
        {
            Guard.WhenArgument<IChatUsersService>(chatUsersService, "Chat users service cannot be null.")
                .IsNull()
                .Throw();

            Guard.WhenArgument<IChatUserFactory>(chatUserFactory, "Chat user factory cannot be null.")
                .IsNull()
                .Throw();

            this.chatUsersService = chatUsersService;
            this.chatUserFactory = chatUserFactory;
        }

        public void SendMessage(string sender, string receiver, string message)
        {
            if (this.Context.User.IsInRole("Administrator") || this.Context.User.IsInRole("Moderator"))
            {
                this.Clients.Group(receiver).addMessage(sender, message);
            }
            else
            {
                this.Clients.Group(sender).addMessage(sender, message);
            }
            this.Clients.Group(SupportRoomName).addMessage(sender, message, receiver);
        }

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.Name;

            if (this.Context.User.IsInRole("Administrator") || this.Context.User.IsInRole("Moderator"))
            {
                var user = this.chatUserFactory.CreateChatUser(name, SupportRole);
                this.chatUsersService.AddChatUser(user);

                this.Groups.Add(this.Context.ConnectionId , SupportRoomName);

                this.Clients.Group(SupportRoomName).connectSupport(name);
            }
            else
            {
                var user = this.chatUserFactory.CreateChatUser(name, ClientRole);
                this.chatUsersService.AddChatUser(user);

                this.Groups.Add(this.Context.ConnectionId, name);

                this.Clients.Group(SupportRoomName).connectClient(name);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = this.Context.User.Identity.Name;

            var user = this.chatUsersService.GetChatUserByEmail(name);
            this.chatUsersService.RemoveChatUser(user);

            if (this.Context.User.IsInRole("Administrator") || this.Context.User.IsInRole("Moderator"))
            {
                this.Clients.Group(SupportRoomName).disconnectSupport(name);
            }
            else
            {
                this.Clients.Group(SupportRoomName).disconnectClient(name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}