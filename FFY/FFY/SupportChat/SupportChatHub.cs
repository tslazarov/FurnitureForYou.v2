using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace FFY.Web.SupportChat
{
    public class SupportChatHub : Hub
    {
        public void SendMessage(string sender, string receiver, string message)
        {
            if (this.Context.User.IsInRole("Administrator"))
            {
                this.Clients.Group(receiver).addMessage(sender, message);
            }
            else
            {
                this.Clients.Group(sender).addMessage(sender, message);
            }
            this.Clients.Group("support").addMessage(sender, message);
        }

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.Name;

            if (this.Context.User.IsInRole("Administrator"))
            {
                Groups.Add(this.Context.ConnectionId ,"support");
            }
            else
            {
                Groups.Add(this.Context.ConnectionId, name);
            }

            return base.OnConnected();
        }
    }
}