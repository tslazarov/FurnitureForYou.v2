using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace FFY.Web.SupportChat
{
    [HubName("supportChatHub")]
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
            this.Clients.Group("support").addMessage(sender, message, receiver);
        }

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.Name;

            if (this.Context.User.IsInRole("Administrator"))
            {
                // repository add
                Groups.Add(this.Context.ConnectionId ,"support");
                this.Clients.Group("support").connectSupport(name);
            }
            else
            {
                // repository add
                Groups.Add(this.Context.ConnectionId, name);
                this.Clients.Group("support").connectClient(name);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = this.Context.User.Identity.Name;
            if (this.Context.User.IsInRole("Administrator"))
            {
                // repository remove
                this.Clients.Group("support").disconnectSupport(name);
            }
            else
            {
                // repository remove
                this.Clients.Group("support").disconnectClient(name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}